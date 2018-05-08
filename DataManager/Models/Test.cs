using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Test
    {
        public Test()
        {
            CurrentTest = new HashSet<CurrentTest>();
        }

        public int Id { get; set; }
        public int TaskId { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }
        public TestTask Task { get; set; }
        public ICollection<CurrentTest> CurrentTest { get; set; }
    }
}
