using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CodeByteForum.ViewModels
{
    public class SettingsViewModel
    {
        public string Email { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Минимум 6 символов")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
