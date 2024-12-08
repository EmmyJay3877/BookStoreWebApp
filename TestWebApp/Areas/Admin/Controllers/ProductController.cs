using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestWeb.DataAccess.Repository;
using TestWeb.Models;
using TestWeb.Models.ViewModels;

namespace TestWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
    {

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public IActionResult Index()
        {
            var productList = _unitOfWork.ProductContext.GetAll(includeProperties: "Category"); 
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {
            var productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = _unitOfWork.CategoryContext.GetAll()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            //update
            productVM.Product = _unitOfWork.ProductContext.Get(p => p.Id == id);
            return View(productVM);
        }

        [HttpPost] 
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productVM.Product.Id == 0)
                {
                    _unitOfWork.ProductContext.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.ProductContext.Update(productVM.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryContext.GetAll();
                return View(productVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.ProductContext.Get(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")] public IActionResult DeleteProduct(int? id)
        {
            Product product = _unitOfWork.ProductContext.Get(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductContext.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
