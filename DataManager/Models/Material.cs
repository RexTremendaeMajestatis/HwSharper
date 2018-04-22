using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public string Url { get; set; }

        public Lecture Lecture { get; set; }
    }
}
