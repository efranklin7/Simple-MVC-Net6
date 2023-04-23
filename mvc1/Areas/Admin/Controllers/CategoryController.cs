using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bulkybook.Models;
using bulkybook.DataAccess;

namespace mvc1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext context;

        public CategoryController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult> Index()
        {
            var list = await context.Categories.ToListAsync();
            return View(list);

        }
        public ActionResult Post()
        {

            return View("Post");

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Post(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "same value");
            }

            context.Categories.Add(obj);
            await context.SaveChangesAsync();
            TempData["succes"] = "created succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
        public ActionResult Edit(int? id)
        {
            Category? result = context.Categories.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Can not be same value");
                return View();
            }
            context.Categories.Update(obj);
            await context.SaveChangesAsync();
            TempData["succes"] = "Edited succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
        public ActionResult Delete(int? id)
        {
            Category? result = context.Categories.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Delete(Category obj)
        {
            var id = context.Categories.Find(obj.Id);
            if (id == null) { return NotFound(); }
            context.Categories.Remove(id);
            await context.SaveChangesAsync();
            TempData["succes"] = "Deleted succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
    }
}
