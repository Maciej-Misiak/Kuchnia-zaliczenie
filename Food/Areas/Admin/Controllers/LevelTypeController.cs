using Food.DataAccess.Repository.IRepository;
using Food.Models;
using Food.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class LevelTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LevelTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<LevelType> objLevelTypeList = _unitOfWork.LevelType.GetAll();
            return View(objLevelTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LevelType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.LevelType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Pomyślnie dodano nowy poziom trudności";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var LevelTypeFromDbFirst = _unitOfWork.LevelType.GetFirstOrDefault(u => u.Id == id);

            if (LevelTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(LevelTypeFromDbFirst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LevelType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.LevelType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Pomyślnie edytowano poziom trudności";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var LevelTypeFromDbFirst = _unitOfWork.LevelType.GetFirstOrDefault(u => u.Id == id);

            if (LevelTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(LevelTypeFromDbFirst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.LevelType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.LevelType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Pomyślnie usunięto poziom trudności";
            return RedirectToAction("Index");
        }
    }
}
