using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class OngoingCourse
    {
        public OngoingCourse()
        {
            CurrentHomework = new HashSet<CurrentHomework>();
            CurrentTest = new HashSet<CurrentTest>();
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
        public ICollection<CurrentHomework> CurrentHomework { get; set; }
        public ICollection<CurrentTest> CurrentTest { get; set; }
        public ICollection<Lecture> Lecture { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
