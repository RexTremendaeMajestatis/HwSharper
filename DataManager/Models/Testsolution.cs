using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public class TestSolution
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TestkId { get; set; }
        public int Status { get; set; }
        public string Url { get; set; }

        public Student Student { get; set; }
        public Test Testk { get; set; }
    }
}
