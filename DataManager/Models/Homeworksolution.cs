using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class HomeworkSolution
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int HomeworkId { get; set; }
        public int Status { get; set; }
        public string Url { get; set; }

        public Homework Homework { get; set; }
        public Student Student { get; set; }
    }
}
