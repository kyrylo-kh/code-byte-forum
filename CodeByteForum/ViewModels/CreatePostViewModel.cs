using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeByteForum.ViewModels
{
    public class CreatePostViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Название обязательно")]
        public string Title { get; set; }
        public string Tags { get; set; }
        [Required(ErrorMessage = "Тема обязательно")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "Текст обязательно")]
        public string Text { get; set; }
    }
}
