using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeByteForum.Models;

namespace CodeByteForum.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public List<Answer> Answers { get; set; }
        public User Sender { get; set; }
    }
}
