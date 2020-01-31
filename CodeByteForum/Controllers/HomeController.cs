using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeByteForum.Models;
using CodeByteForum.Data;
using Microsoft.EntityFrameworkCore;
using CodeByteForum.ViewModels;

namespace CodeByteForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationContext db;

        public HomeController(ILogger<HomeController> _logger, ApplicationContext _db)
        {
            logger = _logger;
            db = _db;
        }

        public async Task<IActionResult> Index(string searchTitle)
        {
            IQueryable<Post> posts = db.Posts.Include(s => s.Sender);
            if(!String.IsNullOrEmpty(searchTitle))
            {
                posts = posts.Where(p => p.Title.Contains(searchTitle));
            }

            PostsListViewModel viewModel = new PostsListViewModel
            {
                Posts = posts.ToList(),
                SearchTitle = searchTitle
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
