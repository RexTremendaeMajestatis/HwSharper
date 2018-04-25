using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class OngoingCourse
    {
        public OngoingCourse()
        {
            Lecture = new HashSet<Lecture>();
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public string TeacherId { get; set; }
        public string GroupId { get; set; }
        public bool? Completed { get; set; }

        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Lecture> Lecture { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
