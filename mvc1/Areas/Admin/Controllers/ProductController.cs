using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bulkybook.Models;
using bulkybook.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using bulkybook.Models.ViewModels;

namespace mvc1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            var list = await context.Products.ToListAsync();
            return View(list);

        }

        public ActionResult Upsert(int? id)
        {
            // id = null : create
            ProductVM productVM = new()
            {

                product = new Product(),
                CategoryList = context.Categories.ToList().Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),

                CoverTypeList = context.CoverTypes.ToList().Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
            };

            if (id==null||id==0)
            {
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            // id = not null : Edit
            else
            {
                return View(id);
            }

           

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // csrf
        public async Task<ActionResult> Upsert(Product? obj,IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
              
            var wwwRootPath =hostEnvironment.WebRootPath;//get the host environmten (wwwrootpath)
            if (file != null)
            {
                var upload=Path.Combine(wwwRootPath,"/images/Products"); // the path to upload
                string fileName=Guid.NewGuid().ToString(); //unique identifier
                var extension = Path.GetExtension(file.FileName); //gets the extension from the file name uploaded

                using (var filestream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                obj.ImgUrl = "/images/products/" + fileName + extension; 
                        
            }
            context.Products.Add(obj);
            await context.SaveChangesAsync();
            TempData["succes"] = "Product added succesfully";
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
