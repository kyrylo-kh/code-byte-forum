using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeByteForum.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public User Author { get; set; }

        public List<string> Tags { get; set; }

        public int AnswerCount { get; set; }

        public List<Answer> Answers { get; set; }

        public string Topic { get; set; }

        public List<File> Files { get; set; }

        public TextFile TextFile { get; set; }

        public string Division { get; set; }

        public int ViewsCount { get; set; }

        public bool isSolved { get; set; }

    }
}
