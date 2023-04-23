using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bulkybook.Models;
using bulkybook.DataAccess;


namespace mvc1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly AppDbContext context;

        public CoverTypeController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult> Index()
        {
            var list = await context.CoverTypes.ToListAsync();
            return View(list);

        }
        public ActionResult Post()
        {

            return View("Post");

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Post(CoverType obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //if (obj.Name == obj..ToString())
            //{
            //    ModelState.AddModelError("Name", "same value");
            //}

            context.CoverTypes.Add(obj);
            await context.SaveChangesAsync();
            TempData["succes"] = "created succesfully";
            return RedirectToAction("Index");
            //return View("index");

        }
        public ActionResult Edit(int? id)
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
        public async Task<ActionResult> Edit(CoverType obj)
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
