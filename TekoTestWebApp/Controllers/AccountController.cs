
using Microsoft.AspNetCore.Mvc;
using TekoTestWebApp.Data;
using TekoTestWebApp.ViewModels;

namespace TekoTestWebApp.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(
            //UserManager<User> userManager,
            //SignInManager<User> signInManager,
            AppDbContext context)
        {
            //_userManager = userManager;
            //_signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        //{
        //    if (!ModelState.IsValid) return View(loginViewModel);

        //    //var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
        //    var user = await 

        //    if (user != null)
        //    {
        //        var has_password = await _userManager.HasPasswordAsync(user);
        //        if (!has_password)
        //        {
        //            await _userManager.RemovePasswordAsync(user);
        //            await _userManager.AddPasswordAsync(user, loginViewModel.Password);
        //        }
        //        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
        //        if (passwordCheck)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
        //            if (result.Succeeded)
        //            {
        //                // Change controller in near future?
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        // Poor decision for incorect password, but time saving..
        //        TempData["Error"] = "Wrong credentials. Please, try again";
        //        return View(loginViewModel);
        //    }
        //    // Poor deciosion for not found user, but time saving..
        //    TempData["Error"] = "Wrong credentials. Please, try again";
        //    return View(loginViewModel);
        //}
    }
}
