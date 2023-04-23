using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bulkybook.Models;
using bulkybook.DataAccess;


namespace mvc1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext db;
        public ProductController(AppDbContext context)
        {
            this.db = context;
        }

        public async Task<ActionResult> Index()
        {
            var list = await db.Products.ToListAsync();
            return View(list);

        }
        
        public ActionResult Upsert(int? id)
        {
            // id = null : create
            if (id==null)
            {
                return View();
            }
            else
            {
                return View(id);
            }

            // id = not null : Edit
           

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Upsert(CoverType obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "Can not be same value");
            //    return View();
            //}
            context.CoverTypes.Update(obj);
            await context.SaveChangesAsync();
            TempData["succes"] = "Edited succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
        public ActionResult Delete(int? id)
        {
            CoverType? result = context.CoverTypes.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Delete(CoverType obj)
        {
            var id = context.CoverTypes.Find(obj.Id);
            if (id == null) { return NotFound(); }
            context.CoverTypes.Remove(id);
            await context.SaveChangesAsync();
            TempData["succes"] = "Deleted succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
    }
}
