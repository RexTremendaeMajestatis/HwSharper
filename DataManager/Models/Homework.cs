using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Homework
    {
        public Homework()
        {
            HomeworkSolution = new HashSet<HomeworkSolution>();
        }

        public int Id { get; set; }
        public int? TaskId { get; set; }
        public int? CourseId { get; set; }

        public Course Course { get; set; }
        public Hometask Task { get; set; }
        public ICollection<HomeworkSolution> HomeworkSolution { get; set; }
    }
}
