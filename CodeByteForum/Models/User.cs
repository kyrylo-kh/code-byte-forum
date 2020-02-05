using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CodeByteForum.Models
{
    public class User : IdentityUser
    {
        public List<Post> Posts { get; set; }
        public List<Answer> Answers { get; set; }
        public string Login { get; set; }
        public AvatarModel Avatar { get; set; } = new AvatarModel { Name = "DefaultAvatar", Path = "/Files/DefaultAvatar.png" };
    }
}
