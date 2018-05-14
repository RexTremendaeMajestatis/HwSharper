using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Course
    {
        public Course()
        {
            Homework = new HashSet<Homework>();
            OngoingCourse = new HashSet<OngoingCourse>();
            Test = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Homework> Homework { get; set; }
        public ICollection<OngoingCourse> OngoingCourse { get; set; }
        public ICollection<Test> Test { get; set; }
    }
}
