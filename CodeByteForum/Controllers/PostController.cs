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
        private readonly UserManager<User> userManager;


        public PostController(ApplicationContext _context, UserManager<User> _userManager)
        {
            this.db = _context;
            this.userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            List<string> _tags = null;

            if (model.Tags != null)
            {
                // Работа с тэгами.
                _tags = model.Tags
                .ToLower() // В нижний регистр.
                .Replace(" ", "") // Удалить все пробелы.
                .Split(',') // Сделать массив подстрок.
                .ToList(); // В List<string>.
            }
            else
            {
                _tags = new List<string>() { "default" };
            }

            // Генерация модели владельца поста.
            User _user = await userManager.FindByNameAsync(User.Identity.Name);

            // Генерация модели нового поста.
            Post new_post = new Post
            {
                Title = model.Title,
                PublishDate = DateTime.Now,
                Sender = _user,
                Tags = _tags,
                AnswerCount = 0,
                Topic = model.Topic,
                Text = model.Text,
                ViewsCount = 0,
                Rating = 0,
                IsSolved = false
            };

            // Добавление нового поста.
            if (new_post != null)
            {
                db.Posts.Add(new_post);

                await db.SaveChangesAsync();
                return Redirect("~/Home/Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Question(int? id)
        {
            if (id != null)
            {
                Post post = await db.Posts
                    .Include(a => a.Answers)
                        .ThenInclude(s => s.Sender)
                    .Include(u => u.Sender)
                        .ThenInclude(s => s.Avatars)
                    .FirstOrDefaultAsync(p => p.Id == id);

                List<Answer> _Answers = post.Answers.OrderByDescending(d => d.PublishDate).ToList();

                User _Sender = post.Sender;

                PostViewModel viewModel = new PostViewModel
                {
                    Post = post,
                    Answers = _Answers,
                    Sender = _Sender
                };

                if (post != null)
                {
                    return View(viewModel);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Question(string _Text, int? id)
        {
            if (_Text == null)
            {
                return Redirect(HttpContext.Request.Path.ToString());
            }
            // Текущий пользователь, который оставляет комментарий.
            User _user = await userManager.FindByNameAsync(User.Identity.Name);

            // Пост, в котором пишется комментарий, включая отправителя.
            Post _currentPost = await db.Posts
                .FirstOrDefaultAsync(p => p.Id == id);

            Answer new_answer = new Answer
            {
                Sender = _user,
                Home = _currentPost,
                PublishDate = DateTime.Now,
                Text = _Text,
                Rating = 0
            };

            if (new_answer != null)
            {
                db.Answers.Add(new_answer);
                _currentPost.AnswerCount += 1;
                await db.SaveChangesAsync();
                return Redirect(HttpContext.Request.Path.ToString());
            }
            return Redirect(HttpContext.Request.Path.ToString());
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            Post post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post != null)
            {
                db.Posts.Remove(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Question", id);
        }
    }
}