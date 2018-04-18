using System;
using System.Collections.Generic;

namespace DataManager.Models
{
    public class TestTask
    {
        public TestTask()
        {
            Test = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Test> Test { get; set; }
    }
}
