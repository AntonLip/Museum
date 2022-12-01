using Microsoft.AspNetCore.Mvc;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces.Service;

namespace Museum.Controllers
{
    public class ExhibitController : Controller
    {
        private readonly IExhibitService _exhibitService;
        public ExhibitController(IExhibitService exhibitService)
        {
            _exhibitService = exhibitService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var models = await _exhibitService.GetAllAsync();
                return View(models);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddExhibitDto model)
        {
            try
            {
                if (model is null)
                    return BadRequest();
                var item = await _exhibitService.AddAsync(model);
                if (item is null)
                    return RedirectToAction("Error", "Home");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();
                var item = await _exhibitService.GetByIdAsync(id);
                if (item is null)
                    return RedirectToAction("Error", "Home");

                return View(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddExhibitDto model)
        {
            try
            {
                if (model is null)
                    return BadRequest();
                var item = await _exhibitService.UpdateAsync(model.Id, model);
                if (item is null)
                    return RedirectToAction("Error", "Home");

                return View(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();
                var item = await _exhibitService.GetByIdAsync(id);
                if (item is null)
                    return RedirectToAction("Error", "Home");

                return View(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();
                var item = await _exhibitService.RemoveAsync(id);
                if (item is null)
                    return RedirectToAction("Error", "Home");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
