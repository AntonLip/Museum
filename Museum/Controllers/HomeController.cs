using Microsoft.AspNetCore.Mvc;
using Museum.Models.DbModels;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces.Service;
using Museum.Models.ViewModels;
using System.Diagnostics;

namespace Museum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExhibitService _exhibitService;
        private readonly IExhibitCategoryService _exhibitCategoryService;
        public HomeController(ILogger<HomeController> logger, IExhibitService exhibitService, IExhibitCategoryService exhibitCategoryService)
        {
            _logger = logger;
            _exhibitService = exhibitService;
            _exhibitCategoryService = exhibitCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IndexViewModel model = new IndexViewModel();
                model.Exhibits = ((List<ExhibitDto>)await _exhibitService.GetAllAsync());
                model.Category = (List<ExhibitCategory>)await _exhibitCategoryService.GetAllAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> Sort(Guid id)
        {
            try
            {
                var model = new IndexViewModel();
                model.Category = (List<ExhibitCategory>)await _exhibitCategoryService.GetAllAsync();
                model.Exhibits = _exhibitService.GetByCategoryId(id);
                return View("Index", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
            
        }
        public async Task<IActionResult> FindAsync(string name)
        {
            try
            {
                var model = new IndexViewModel();
                model.Category = (List<ExhibitCategory>)await _exhibitCategoryService.GetAllAsync();
                model.Exhibits = _exhibitService.GetByPartName(name);
                return View("Index", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}