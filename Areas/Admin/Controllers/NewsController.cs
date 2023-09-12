using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using ShopApp.Models;
using ShopApp.Models.Common;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/news/[action]/{id?}")]
    public class NewsController : Controller
    {
        // GET: NewsController
        private readonly ShopContext _context;
        private readonly IFileProvider fileProvider;
        IWebHostEnvironment _env;
        public NewsController(ShopContext context, IWebHostEnvironment env, IFileProvider fileprovider)
        {
            _context = context;
            _env = env;
            fileProvider = fileprovider;
        }
        public ActionResult Index()
        {
            var news = _context.news.ToList();
            return View(news);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            var cate = (from c in _context.news
                        where c.Id == id
                        select c).FirstOrDefault();
            if (cate == null) return NotFound();
            return View(cate);
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            var cate = (from c in _context.categories
                        select c).ToList();
            var categoryIds = new SelectList(cate, "Id", "Title", -1);
            ViewData["CategoryIds"] = categoryIds;
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title, Description, Detail, Image, SeoTitle, SeoDescription, SeoKeywords, Slug,CategoryID")] New news, IFormFile file)
        {

            if (news.Slug == null)
            {
                news.Slug = ConvertSlug.GenerateSlug(news.Title);
            }
            Console.WriteLine(file != null);
            Console.WriteLine("file " + file);
            if (file != null)
            {
                // Create a File Info 
                FileInfo fi = new FileInfo(file.FileName);

                // This code creates a unique file name to prevent duplications 
                // stored at the file location
                var newFilename = news.Slug + "_" + String.Format("{0:d}",
                                  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                var webPath = _env.WebRootPath;
                Console.WriteLine(newFilename);
                var path = Path.Combine("", webPath + @"\files\" + newFilename);

                // IMPORTANT: The pathToSave variable will be save on the column in the database
                var pathToSave = @"/files/" + newFilename;

                // This stream the physical file to the allocate wwwroot/ImageFiles folder
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // This save the path to the record
                news.Image = pathToSave;
            }
            if (ModelState.IsValid)
            {
                news.CreatedDate = news.ModifierDate = DateTime.Now;
                // tam thoi
                news.CreatedBy = news.ModifierBy = "Cuong";
                _context.news.Add(news);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            else return View();
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            var news = (from n in _context.news
                        where n.Id == id
                        select n).FirstOrDefault();
            var cate = (from c in _context.categories
                        select c).ToList();
            var categoryIds = new SelectList(cate, "Id", "Title", news.CategoryID);
            ViewData["CategoryIds"] = categoryIds;

            return View(news);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Title, Description, Detail, Image, SeoTitle, SeoDescription, SeoKeywords, Slug,CategoryID")] New news, IFormFile file)
        {
            var newEdit = (from c in _context.news
                           where c.Id == id
                           select c).FirstOrDefault();
            if (newEdit == null) return NotFound();
            if (newEdit.Slug == null)
            {
                newEdit.Slug = ConvertSlug.GenerateSlug(newEdit.Title);
            }
            Console.WriteLine("a" + file);
            if (file != null)
            {

                // Create a File Info 
                FileInfo fi = new FileInfo(file.FileName);

                // This code creates a unique file name to prevent duplications 
                // stored at the file location
                var newFilename = news.Slug + "_" + String.Format("{0:d}",
                                  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                var webPath = _env.WebRootPath;
                var path = Path.Combine("", webPath + @"\files\" + newFilename);

                // IMPORTANT: The pathToSave variable will be save on the column in the database
                var pathToSave = @"/files/" + newFilename;

                // This stream the physical file to the allocate wwwroot/ImageFiles folder
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // This save the path to the record
                news.Image = pathToSave;
            }
            else
            {
                news.Image = newEdit.Image;
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine(news.Image);
                newEdit.Title = news.Title;
                newEdit.Description = news.Description;
                newEdit.CategoryID = news.CategoryID;
                newEdit.Image = news.Image;
                newEdit.SeoTitle = news.SeoTitle;
                newEdit.SeoKeywords = news.SeoKeywords;
                newEdit.SeoDescription = news.SeoDescription;
                newEdit.ModifierDate = DateTime.Now;
            }
            _context.Update(newEdit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var news = (from n in _context.news
                        where n.Id == id
                        select n).FirstOrDefault();
            return View(news);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news = (from c in _context.news
                        where c.Id == id
                        select c).FirstOrDefault();
            if (news == null) return NotFound();
            _context.news.Remove(news);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
