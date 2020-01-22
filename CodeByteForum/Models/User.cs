﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CodeByteForum.Models
{
    public class User : IdentityUser
    {
        public List<Post> Posts;
    }
}
