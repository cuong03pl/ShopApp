using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopApp.Models;
using ShopApp.Models.Common;
using X.PagedList;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/posts/[action]/{id?}")]
    public class PostsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IFileProvider fileProvider;
        IWebHostEnvironment _env;
        public PostsController(ShopContext context, IWebHostEnvironment env, IFileProvider fileprovider)
        {
            _context = context;
            _env = env;
            fileProvider = fileprovider;
        }
        // GET: Posts
        public ActionResult Index(int? page, string? searchString)
        {
            var posts = from p in _context.posts
                        select p;
            int pageSize = 5;
            int pageCurrent = page ?? 1;
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.Title.Contains(searchString));
            }
            return View(posts.ToPagedList(pageCurrent, pageSize));
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            var post = (from p in _context.posts
                        where p.Id == id
                        select p).FirstOrDefault();
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            var cate = (from c in _context.categories
                        select c).ToList();
            var cateIds = new SelectList(cate, "Id", "Title", -1);
            ViewData["cateIds"] = cateIds;
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title", "Description", "Detail", "Image", "SeoTitle", "SeoDescription", "SeoKeywords", "CategoryID")] Post post, IFormFile file)
        {
            Console.WriteLine(post.CategoryID);
            if (file != null)
            {
                // Create a File Info 
                FileInfo fi = new FileInfo(file.FileName);

                // This code creates a unique file name to prevent duplications 
                // stored at the file location
                var newFilename = ConvertSlug.GenerateSlug(post.Title) + "_" + String.Format("{0:d}",
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
                post.Image = pathToSave;
            }

            if (ModelState.IsValid)
            {
                post.CreatedDate = post.ModifierDate = DateTime.Now;
                // tam thoi
                post.CreatedBy = post.ModifierBy = "Cuong";
                _context.posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        //// GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            var post = (from n in _context.posts
                        where n.Id == id
                        select n).FirstOrDefault();
            var cate = (from c in _context.categories
                        select c).ToList();
            var cateIds = new SelectList(cate, "Id", "Title", post?.CategoryID);
            ViewData["cateIds"] = cateIds;
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Title", "Description", "Detail", "Image", "SeoTitle", "SeoDescription", "SeoKeywords", "CategoryID")] Post post, IFormFile file)
        {
            var postEdit = (from n in _context.posts
                            where n.Id == id
                            select n).FirstOrDefault();
            if (postEdit == null)
            {
                return NotFound();
            }
            if (file != null)
            {
                // Create a File Info 
                FileInfo fi = new FileInfo(file.FileName);

                // This code creates a unique file name to prevent duplications 
                // stored at the file location
                var newFilename = ConvertSlug.GenerateSlug(post.Title) + "_" + String.Format("{0:d}",
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
                post.Image = pathToSave;
            }
            else
            {
                post.Image = postEdit.Image;
            }

            if (ModelState.IsValid)
            {
                postEdit.ModifierBy = "Cuong";
                postEdit.Title = post.Title;
                postEdit.Description = post.Description;
                postEdit.Detail = post.Detail;
                postEdit.CategoryID = post.CategoryID;
                postEdit.Image = post.Image;
                postEdit.SeoTitle = post.SeoTitle;
                postEdit.SeoKeywords = post.SeoKeywords;
                postEdit.SeoDescription = post.SeoDescription;
                postEdit.ModifierDate = DateTime.Now;
                _context.posts.Update(postEdit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            Console.WriteLine("id" + id);
            if (id == null)
            {
                return NotFound();
            }

            var post = (from n in _context.posts
                        where n.Id == id
                        select n).FirstOrDefault();
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            if (id == null) return NotFound();
            var post = (from p in _context.posts
                        where p.Id == id
                        select p).FirstOrDefault();
            if (post != null)
            {
                _context.posts.Remove(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
