using CodeByteForum.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CodeByteForum.ViewModels
{
    public class ProfileViewModel
    {
     //   public User User { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public List<Post> Posts { get; set; }
        public List<Answer> Answers { get; set; }
        public string Tab { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
