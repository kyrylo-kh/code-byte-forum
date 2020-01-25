using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CodeByteForum.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public User Sender { get; set; }


        public List<string> Tags { get; set; }

        public int AnswerCount { get; set; }

        public List<Answer> Answers { get; set; }

        public string Topic { get; set; }

        public string Text { get; set; }

        public string Division { get; set; }

        public int ViewsCount { get; set; }

        public int Rating { get; set; }
 
        public bool IsSolved { get; set; }
    }
}
