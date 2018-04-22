using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Student
    {
        public Student()
        {
            HomeworkSolution = new HashSet<HomeworkSolution>();
            StudentCourse = new HashSet<StudentCourse>();
            TestSolution = new HashSet<TestSolution>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }

        public ICollection<HomeworkSolution> HomeworkSolution { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
        public ICollection<TestSolution> TestSolution { get; set; }
    }
}
