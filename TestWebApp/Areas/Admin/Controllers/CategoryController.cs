using Microsoft.AspNetCore.Mvc;
using TestWeb.DataAccess.Data;
using TestWeb.DataAccess.Repository;
using TestWeb.Models;

namespace TestWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(UnitOfWork unitOfWork) : Controller
    {

        private readonly UnitOfWork _unitOfWork = unitOfWork;

		public IActionResult Index()
        {
            var categoryList = _unitOfWork.CategoryContext.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryContext.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryContext.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryContext.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryContext.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            Category? category = _unitOfWork.CategoryContext.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryContext.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
