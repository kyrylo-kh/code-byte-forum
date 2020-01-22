using System;
using System.Collections.Generic;
using System.IO;

namespace CodeByteForum.Models
{
    public class Answer
    {
        public int Id { get; set; }
        // public User Sender { get; set; }
        public DateTime PublishDate { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public List<FileInfo> Files { get; set; }
    }
}
