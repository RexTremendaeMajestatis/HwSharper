using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public class Teacher
    {
        public Teacher()
        {
            OngoingCourse = new HashSet<OngoingCourse>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }

        public ICollection<OngoingCourse> OngoingCourse { get; set; }
    }
}
