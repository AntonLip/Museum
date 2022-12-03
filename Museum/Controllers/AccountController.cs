using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Museum.Models.ViewModels.AccountViewModels;
using Museum.Models.DbModels;
using Museum.Models.ViewModels;

namespace Museum.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync()
        {            
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViweModel registerViweModel)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerViweModel.Email,
                    Email = registerViweModel.Email,
                    FirstName = registerViweModel.FirstName,
                    LastName = registerViweModel.LastName

                };
                var result = _userManager.CreateAsync(user, registerViweModel.Password);
                if (result.Result.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Админ"))
                    {
                        return RedirectToAction("ListUsers", "Admin");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Home");
                }
                foreach (var er in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, er.Description);
                }
                return View(registerViweModel);
            }
            catch (Exception)
            {
                return View(registerViweModel);
            }
               
           
            
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LogginViewModel loginViewModel, string returnUrl)
        {
            
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if(user is null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Log In Attempt");
                    return View(loginViewModel);
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
               
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Log In Attempt");
                }
            
            return View(loginViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already use");
            }
        }

        public async Task<IActionResult> Info()
        {
            try
            {                
                var user = await _userManager.GetUserAsync(User);                
                UserViewModel model = new UserViewModel
                {
                    Email = user.Email, 
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber
                };
                return View(model);
            }
            catch(Exception ex)
            {
                ErrorViewModel modelError = new ErrorViewModel();
                //modelError.Message = ex.Message;
                return RedirectToAction("Error", "Home", modelError);
            }
        }

    }
}
