using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Museum.Models.DbModels;
using Museum.Models.Interfaces.Service;

namespace Museum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExhibitCategoryController : Controller
    {
        private readonly IExhibitCategoryService _exhibitCategoryService;
        public ExhibitCategoryController(IExhibitCategoryService exhibitCategoryService)
        {
            _exhibitCategoryService = exhibitCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var models = await _exhibitCategoryService.GetAllAsync();
                return View(models);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExhibitCategory model)
        {
            try
            {
                if (model is null)
                    return BadRequest();
                var item = await _exhibitCategoryService.AddAsync(model);
                return RedirectToAction("Index");  
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();
            var item = await _exhibitCategoryService.GetByIdAsync(id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ExhibitCategory model)
        {
            try
            {                
                var updatedItem = await _exhibitCategoryService.UpdateAsync(model.Id, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var item = await _exhibitCategoryService.RemoveAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
