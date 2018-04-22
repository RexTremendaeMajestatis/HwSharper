using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Announcement
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public string Message { get; set; }

        public Lecture Lecture { get; set; }
    }
}
