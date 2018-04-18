using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public class OngoingCourse
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }
        public string GroupId { get; set; }
        public bool? Completed { get; set; }

        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
    }
}
