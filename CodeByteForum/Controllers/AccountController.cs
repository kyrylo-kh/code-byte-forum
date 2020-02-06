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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CodeByteForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            ApplicationContext context, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
            _environment = environment;
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
                    db.Avatars.Add(new AvatarModel { Owner = user, Name = "DefaultAvatar", Path = "~/avatars/DefaultAvatar.png" });
                    //user.Avatars.Add(new AvatarModel { Name = "DefaultAvatar", Path = "~/avatars/DefaultAvatar.png" });
                    await db.SaveChangesAsync();
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
                .Include(u => u.Posts)
                .Include(u => u.Answers)
                .Include(u => u.Avatars)
                .FirstOrDefaultAsync(u => u.Login == login);
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
                    .Include(u => u.Avatars)
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
                        return View("Profile", model);
                    }
                }
                if (model.NewEmail != _user.Email)
                {
                    var result = await _userManager.SetEmailAsync(_user, model.NewEmail);
                    if (!result.Succeeded)
                        ModelState.AddModelError("Email", "Не пошло...");
                }
                if (model.AvatarFile != null)
                {
                    if (model.AvatarFile.ContentType == "image/jpeg" || model.AvatarFile.ContentType == "image/pjpeg"
                        || model.AvatarFile.ContentType == "image/png" || model.AvatarFile.ContentType == "image/svg+xml")
                    {
                        string path = "/avatars/" + model.AvatarFile.FileName;
                        using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                        {
                            await model.AvatarFile.CopyToAsync(fileStream);
                        }
                        _user.Avatars.Add(new AvatarModel { Name = model.AvatarFile.FileName, Path = path, Owner = _user });
                        
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("AvatarFile", "Вы выбрали не фото");
                    }
                    
                }
                return View("Profile", model);
            }
            return View("Profile", model);
        }
    }
}