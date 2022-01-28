using Food.DataAccess.Repository.IRepository;
using Food.Models;
using Food.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Food.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
        
    {      
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Dish> dishList = _unitOfWork.Dish.GetAll(includeProperties:"Category,LevelType"); 

            return View(dishList);
        }

        public IActionResult Details(int id)
        {
            DetailsCart cartObj = new()
            {
                Dish = _unitOfWork.Dish.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,LevelType")
            };
            return View(cartObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}