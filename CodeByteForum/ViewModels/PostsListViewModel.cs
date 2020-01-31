using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeByteForum.Models;

namespace CodeByteForum.ViewModels
{
    public class PostsListViewModel
    {
        public IEnumerable<Post> Posts;
        
        // Filter.
        public string SearchTitle { get; set; }
    }
}
