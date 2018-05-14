using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Lecture
    {
        public Lecture()
        {
            Announcement = new HashSet<Announcement>();
            Material = new HashSet<Material>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }

        public OngoingCourse Course { get; set; }
        public ICollection<Announcement> Announcement { get; set; }
        public ICollection<Material> Material { get; set; }
    }
}
