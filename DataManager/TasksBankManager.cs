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

        public static void DeleteRelatedHwInfo(HwProj_DBContext db, int id)
        {
            var relatedHw = db.Homework.Where(hw => hw.TaskId == id);
            var relatedSolutions = db.HomeworkSolution.Where(s => s.Homework.TaskId == id);
            db.HomeworkSolution.RemoveRange(relatedSolutions);
            db.Homework.RemoveRange(relatedHw);
        }
        
        public static void DeleteRelatedTestInfo(HwProj_DBContext db, int id)
        {
            var relatedHw = db.Test.Where(test => test.TaskId == id);
            var relatedSolutions = db.TestSolution.Where(s => s.Test.TaskId == id);
            db.TestSolution.RemoveRange(relatedSolutions);
            db.Test.RemoveRange(relatedHw);
        }
        
        public static void DeleteHometask(int taskId)
        {
            using (var db = new HwProj_DBContext())
            {
                var taskToDelete =
                    db.Hometask.First(task => task.Id == taskId);
                DeleteRelatedHwInfo(db, taskId);
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
                DeleteRelatedTestInfo(db, taskId);
                db.TestTask.Remove(taskToDelete);
                db.SaveChanges();
            }
        }

    }
}