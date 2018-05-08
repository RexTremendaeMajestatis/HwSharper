using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class CurrentHomework
    {
        public CurrentHomework()
        {
            HomeworkSolution = new HashSet<HomeworkSolution>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int HwId { get; set; }
        public DateTime Date { get; set; }

        public OngoingCourse Course { get; set; }
        public Homework Hw { get; set; }
        public ICollection<HomeworkSolution> HomeworkSolution { get; set; }
    }
}
