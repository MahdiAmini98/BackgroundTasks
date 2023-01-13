using Hangfire.Infrastructures.Service;
using Hangfire.Models.Entities;
using Hangfire.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly SmsService _smsService;
        private readonly EmailService _emailService;
        public AccountController(UserManager<User> userManager, SignInManager<User> singInManager, EmailService emailService, SmsService smsService)
        {
            _singInManager = singInManager;
            _userManager = userManager;
            _emailService = emailService;
            _smsService = smsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(model);
            }
            _emailService.SendWelcomeEmail(model.Email);
            _smsService.SendWelcomeSMS(model.PhoneNumber);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
            {
                ModelState.TryAddModelError("Not Found", "User Not Found");
                return View(model);
            }
            var result = await _singInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError("NotCorrect", "User or Password not Correct");
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
