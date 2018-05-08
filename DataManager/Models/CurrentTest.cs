using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class CurrentTest
    {
        public CurrentTest()
        {
            TestSolution = new HashSet<TestSolution>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TestId { get; set; }
        public DateTime Date { get; set; }

        public OngoingCourse Course { get; set; }
        public Test Test { get; set; }
        public ICollection<TestSolution> TestSolution { get; set; }
    }
}
