using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            OngoingCourse = new HashSet<OngoingCourse>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public ICollection<OngoingCourse> OngoingCourse { get; set; }
    }
}
