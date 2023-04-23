using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bulkybook.Models;
using bulkybook.DataAccess;


namespace mvc1.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    
    {
        private readonly AppDbContext context;

        public CoverTypeController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult> Index()
        {
            var list = await context.Products.ToListAsync();
            return View(list);

        }
        public ActionResult Post()
        {

            return View("Post");

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Post(Product obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //if (obj.Name == obj..ToString())
            //{
            //    ModelState.AddModelError("Name", "same value");
            //}

            context.Products.Add(obj);
            await context.SaveChangesAsync();
            TempData["succes"] = "created succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
        public ActionResult Edit(int? id)
        {
            Product? result = context.Products.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Edit(Product obj)
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
            context.Products.Update(obj);
            await context.SaveChangesAsync();
            TempData["succes"] = "Edited succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
        public ActionResult Delete(int? id)
        {
            Product? result = context.Products.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Delete(Product obj)
        {
            var id = context.Products.Find(obj.Id);
            if (id == null) { return NotFound(); }
            context.Products.Remove(id);
            await context.SaveChangesAsync();
            TempData["succes"] = "Deleted succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
    }
}
