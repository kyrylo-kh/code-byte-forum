using CodeByteForum.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CodeByteForum.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public string Tab { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        //public bool isInfoChanged { get; set; }
    }
}
