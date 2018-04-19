using DataManager.Models;

namespace DataManager
{
    public static class TasksManager
    {
        public static void AddNewHomeTask(string title, string description)
        {
            using (var db = new HwProj_DBContext())
            {
                db.Hometask.Add(new Hometask() {Title = title, Description = description});
            }
        }
        
        public static void AddNewTestTask(string title, string description)
        {
            using (var db = new HwProj_DBContext())
            {
                db.TestTask.Add(new TestTask() {Title = title, Description = description});
            }
        }
        
        /*public static void DeleteHomeTask(string title, string description)
        {
            using (var db = new HwProj_DBContext())
            {
                db.Hometask.Remove(task => task.Title == title);
            }
        }*/

    }
}