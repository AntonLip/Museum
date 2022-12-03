using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Museum.Models.DbModels;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces;
using Museum.Models.Interfaces.Service;

namespace Museum.Controllers
{

    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManagement;
        public TicketController(ITicketService ticketService, UserManager<ApplicationUser> userManagement, IFileService fileService)
        {
            _ticketService = ticketService;
            _userManagement = userManagement;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var models = await _ticketService.GetAllAsync();
                    return View(models);
                }
                else if (User.IsInRole("User"))
                {
                    var userId = _userManagement.GetUserId(User);
                    var models = _ticketService.GetByUserId(userId);
                    return View(models);
                }
                else
                {
                    return NotFound();
                }

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
        public async Task<IActionResult> Create(Ticket model)
        {
            try
            {
                if (User.IsInRole("User"))
                {
                    model.Price = 5;
                    model.UserId = _userManagement.GetUserId(User);
                }
                var item = await _ticketService.AddAsync(model);
                if (item is null)
                    return BadRequest();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var item = await _ticketService.GetByIdAsync(id);
                return View(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Ticket model)
        {
            try
            {
                if (model is null)
                    return BadRequest();
                var item = await _ticketService.UpdateAsync(model.Id, model);
                if (item is null)
                    return NotFound();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var item = await _ticketService.GetByIdAsync(id);
                if (item is null)
                    return NotFound();
                return View(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();
                var item = _ticketService.RemoveAsync(id);
                if (item is null)
                    return NotFound();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> Save(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();
                var item = await _ticketService.GetByIdAsync(id);
                if (item is null)
                    return NotFound();
                FileDto file = _fileService.GetTicketInPDF(item);
                return File(file.FileBytes, file.FileType, fileDownloadName: file.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> Statistics()
        //{
        //    var items = new List<string> { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        //    ViewBag.Months = new SelectList(items);
        //    return View();
        //}
        //[HttpPost]
        public async Task<IActionResult> Statistics()
        {
            try
            {
                List<StatisticDto> model = await _ticketService.GetStatisticDataCurrentYear();
                
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller {nameof(ExhibitController)}, Messadge {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
