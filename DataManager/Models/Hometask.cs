using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public class Hometask
    {
        public Hometask()
        {
            Homework = new HashSet<Homework>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Homework> Homework { get; set; }
    }
}
