using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public class Course
    {
        public Course()
        {
            Homework = new HashSet<Homework>();
            Lecture = new HashSet<Lecture>();
            OngoingCourse = new HashSet<OngoingCourse>();
            StudentCourse = new HashSet<StudentCourse>();
            Test = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Homework> Homework { get; set; }
        public ICollection<Lecture> Lecture { get; set; }
        public ICollection<OngoingCourse> OngoingCourse { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
        public ICollection<Test> Test { get; set; }
    }
}
