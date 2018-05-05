using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public static class TasksManager
    {
        public static bool AddNewHomeTask(string title, string description)
        {
            using (var db = new HwProj_DBContext())
            {
                if (db.Hometask.Any(task => task.Description == description && task.Title == title))
                    return false;
                else
                {
                    db.Hometask.Add(new Hometask() {Title = title, Description = description});
                    db.SaveChanges();
                    return true;
                }
            }
        }
        
        public static bool AddNewTestTask(string title, string description)
        {
            using (var db = new HwProj_DBContext())
            {
                if (db.Hometask.Any(task => task.Description == description && task.Title == title))
                    return false;
                else
                {
                    db.TestTask.Add(new TestTask() {Title = title, Description = description});
                    db.SaveChanges();
                    return true;
                }
            }
        }
        
        public static void DeleteHometask(int taskId)
        {
            using (var db = new HwProj_DBContext())
            {
                var taskToDelete =
                    db.Hometask.First(task => task.Id == taskId);
                db.Hometask.Remove(taskToDelete);
                db.SaveChanges();
            }
        }
        
        public static void DeleteTestTask(int taskId)
        {
            using (var db = new HwProj_DBContext())
            {
                var taskToDelete =
                    db.TestTask.First(task => task.Id == taskId);
                db.TestTask.Remove(taskToDelete);
                db.SaveChanges();
            }
        }

    }
}