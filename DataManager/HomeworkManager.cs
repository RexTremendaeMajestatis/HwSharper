using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public class HomeworkManager
    {
        public static void AddHomework(int taskId, int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedTask = db.Hometask.First(task => task.Id == taskId);
                var relatedCourse = db.Course.First(course => course.Id == courseId);
                var toAdd = new Homework() { TaskId = taskId, 
                                             CourseId = courseId,
                                             Course = relatedCourse,
                                             Task = relatedTask
                                           };
                db.Homework.Add(toAdd);
                relatedCourse.Homework.Add(toAdd);
                db.SaveChanges();
            }
        }
        
        public static void DeleteHomework(int hwId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.Homework.First(task => task.Id == hwId);
                db.Homework.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}