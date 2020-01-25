using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeByteForum.ViewModels;
using CodeByteForum.Models;
using CodeByteForum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeByteForum.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User>  userManager; // -identityuser = user
        
        
        public PostController(ApplicationContext _context, UserManager<User> _userManager) // -identityuser = user
        {
            this.db = _context; 
            this.userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            // Работа с тэгами.
            List<string> _tags = model.Tags
                .ToLower() // В нижний регистр.
                .Replace(" ", "") // Удалить все пробелы.
                .Split(',') // Сделать массив подстрок.
                .ToList(); // В List<string>.

            // Генерация модели владельца поста.
            User _user = await userManager.FindByNameAsync(User.Identity.Name); // !!!!!!

            // Генерация модели нового поста.
            Post new_post = new Post {
                Title = model.Title, PublishDate = DateTime.Now,
                Sender = _user, // !!!!!!!!!!!!!!!!!!!!!!!!!
                Tags = _tags,
                AnswerCount = 0, Topic = model.Topic,
                Text = model.Text, ViewsCount = 0,
                Rating = 0, IsSolved = false};

            // Добавление нового поста.
            if(new_post != null)
            {
                db.Posts.Add(new_post);
                
                await db.SaveChangesAsync();
                return RedirectToAction("/Home/Index");
            }
            return View();
        }

        public async Task<IActionResult> Question(int? id)
        {
            if(id != null)
            {
                Post post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if(post != null)
                {
                    return View(post);
                }
            }
            return NotFound();
        }
    }
}   