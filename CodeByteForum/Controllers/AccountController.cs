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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings (ProfileViewModel model)
        {
            User _user = await db.Users
                    .Include(u => u.Answers)
                    .Include(u => u.Posts)
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            model.User = _user;
            if (ModelState.IsValid)
            {

                if (model.OldPassword != null && model.NewPassword != null)
                {
                    if (await _userManager.CheckPasswordAsync(_user, model.OldPassword))
                    {
                        await _userManager.ChangePasswordAsync(_user, model.OldPassword, model.NewPassword);
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Вы ввели неправильный пароль");
                    }
                    return View("Profile", model);
                }
                if (model.User.Email != _user.Email)
                {
                    await _userManager.SetEmailAsync(_user, model.User.Email);
                }
                return View("Profile", model);
            }
            return View("Profile", model);
        }
    }
}