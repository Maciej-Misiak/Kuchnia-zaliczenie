using Food.DataAccess.Repository.IRepository;
using Food.Models;
using Food.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Food.Utility;

namespace Food.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)] 
    public class DishController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
     
        public DishController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
    
        public IActionResult Index()
        {           
            return View();
        }      
        public IActionResult Upsert(int? id)
        {
            DishVM dishVM = new()
            {
                Dish = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                LevelTypeList = _unitOfWork.LevelType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
           
            if (id == null || id == 0)
            {           
                return View(dishVM);
            }
            else
            {
                dishVM.Dish = _unitOfWork.Dish.GetFirstOrDefault(u => u.Id == id);
                return View(dishVM);
            }                
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DishVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file!=null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\dish");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Dish.ImageUrl!=null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Dish.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStreams = new FileStream(Path.Combine(uploads,fileName+extension),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Dish.ImageUrl = @"\images\dish\" + fileName + extension;
                }
                if (obj.Dish.Id == 0)
                {
                    _unitOfWork.Dish.Add(obj.Dish);
                }
                else
                {
                    _unitOfWork.Dish.Update(obj.Dish);
                }              
                _unitOfWork.Save();
                TempData["success"] = "Potrawę dodano pomyślnie";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
     
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var dishList = _unitOfWork.Dish.GetAll(includeProperties:"Category,LevelType");
            return Json(new { data = dishList });
        }

        [HttpDelete]      
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Dish.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Błąd przy usuwaniu" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Dish.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Usuwanie powiodło się" });
        }
        #endregion
    }
    
}
