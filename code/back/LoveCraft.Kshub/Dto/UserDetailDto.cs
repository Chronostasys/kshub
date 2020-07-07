﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    public class UserDetailDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string SchoolName { get; set; }
        public string Introduction { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
    public class AddUserDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string SchoolName { get; set; }
        public string Introduction { get; set; }
        public string Email { get; set; }
    }
    public class LogInDto
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}