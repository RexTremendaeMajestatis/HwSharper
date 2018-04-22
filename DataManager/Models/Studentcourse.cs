using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public OngoingCourse Course { get; set; }
        public Student Student { get; set; }
    }
}
