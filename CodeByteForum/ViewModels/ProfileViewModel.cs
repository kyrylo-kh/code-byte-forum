using CodeByteForum.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CodeByteForum.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        [DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Минимум 6 символов")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        public IFormFile AvatarFile { get; set; }
    }
}
