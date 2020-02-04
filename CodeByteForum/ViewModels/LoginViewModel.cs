using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeByteForum.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
