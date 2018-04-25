using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class TestSolution
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int TestId { get; set; }
        public int Status { get; set; }
        public string Url { get; set; }

        public Student Student { get; set; }
        public Test Test { get; set; }
    }
}
