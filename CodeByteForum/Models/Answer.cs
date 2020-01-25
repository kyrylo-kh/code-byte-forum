using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace CodeByteForum.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public User Sender { get; set; }

        public int PostId { get; set; }
        public Post Home { get; set; }

        public DateTime PublishDate { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
