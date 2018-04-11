using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Userdata
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool? IsTeacher { get; set; }
    }
}
