using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bulkybook.Models;
using bulkybook.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mvc1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext context;
        public ProductController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult> Index()
        {
            var list = await context.Products.ToListAsync();
            return View(list);

        }
        
        public ActionResult Upsert(int? id)
        {
            Product product = new Product();
            IEnumerable<SelectListItem> CategoryList= context.Categories.ToList().Select(c =>
            new SelectListItem {Text=c.Name,Value=c.Id.ToString()});

            IEnumerable<SelectListItem> CoverTypeList = context.CoverTypes.ToList().Select(c =>
            new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            // id = null : create
            if (id==null||id==0)
            {
                ViewBag.CategoryList = CategoryList;
                ViewData["CoverTypeList"] = CoverTypeList;
                return View(product);
            }
            else
            {
                return View(id);
            }

            // id = not null : Edit
           

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Upsert(Product obj)
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
