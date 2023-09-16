using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using ShopApp.Models.Common;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/category/[action]/{id?}")]
    public class CategoryController : Controller
    {

        private readonly ShopContext _context;

        public CategoryController(ShopContext context)
        {
            _context = context;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var categories = _context.categories.ToList();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var cate = (from c in _context.categories
                        where c.Id == id
                        select c).FirstOrDefault();
            if (cate == null) return NotFound();
            return View(cate);
        }

        // GET: CategoryController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Title,Description, Position, SeoTitle, SeoDescription, SeoKeywords, Slug")] Category category)
        {
           
            if(category.Slug == null)
            {
                category.Slug = ConvertSlug.GenerateSlug(category.Title);
            }

            if (ModelState.IsValid)
            {
                category.CreatedDate = category.ModifierDate = DateTime.Now;
                // tam thoi
                category.CreatedBy = category.ModifierBy = "Cuong";
                _context.categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cate = (from c in _context.categories
                        where c.Id == id
                        select c).FirstOrDefault();
            if (cate == null) return NotFound();
            return View(cate);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Title,Description, Position, SeoTitle, SeoDescription, SeoKeywords, Slug")] Category category)
        {
            var cate = (from c in _context.categories
                        where c.Id == id
                        select c).FirstOrDefault();
            if (cate == null) return NotFound();
            if (category.Slug == null)
            {
                category.Slug = ConvertSlug.GenerateSlug(category.Title);
            }
            if (ModelState.IsValid)
            {
                cate.Title = category.Title;
                cate.Description = category.Description;
                cate.Position = category.Position;
                cate.SeoTitle = category.SeoTitle;
                cate.SeoKeywords = category.SeoKeywords;
                cate.SeoDescription = category.Description;
                cate.ModifierDate = DateTime.Now;
            }
            _context.Update(cate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var cate = (from c in _context.categories
                        where c.Id == id
                        select c).FirstOrDefault();
            if (cate == null) return NotFound();
            return View(cate);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cate = (from c in _context.categories
                        where c.Id == id
                        select c).FirstOrDefault();
            if (cate == null) return NotFound();
            _context.categories.Remove(cate);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
