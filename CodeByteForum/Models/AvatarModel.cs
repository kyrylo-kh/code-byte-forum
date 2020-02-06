using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeByteForum.Models
{
    public class AvatarModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public User Owner { get; set; }
    }
}
