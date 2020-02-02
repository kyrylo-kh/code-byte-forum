using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeByteForum.Models;
using CodeByteForum.ViewModels;
using CodeByteForum.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeByteForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Login,
                                       Login = model.Login};
                // Добавляем пользователя.
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Установка куки.
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return RedirectToAction("Login");
                var result =
                    await _signInManager
                    .PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile(string login)
        {
            login = string.IsNullOrEmpty(login) ? User.Identity.Name : login;
            User _user = await db.Users
                .Include(p => p.Posts)
                .Include(a => a.Answers)
                .FirstOrDefaultAsync(i => i.Login == login);
            if (_user != null)
            {
                return View(new ProfileViewModel { 
                    User = _user,
                    
                });
            }
            return NotFound();
        }    

        public async Task<IActionResult> Settings()
        {
            User _user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(new SettingsViewModel
            {
                Email = _user.Email,
                Login = _user.Login
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings (SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                User _user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (await _userManager.CheckPasswordAsync(_user, model.OldPassword)) {
                    var result = await _userManager.ChangePasswordAsync(_user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        if (_user.Email != model.Email)
                            await _userManager.SetEmailAsync(_user, model.Email);
                        if (_user.Login != model.Login)
                            await _userManager.SetUserNameAsync(_user, model.Login);
                        await _userManager.UpdateAsync(_user);
                        return RedirectToAction("Profile");
                    }
                }
            }
            return View(model);
        }
    }
}