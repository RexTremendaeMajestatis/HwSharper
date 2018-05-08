using System;
using System.Collections.Generic;
using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public class HomeworkManager
    {
        public static void AddHomeworkToCourse(int taskId, int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedTask = db.Hometask.First(task => task.Id == taskId);
                var relatedCourse = db.Course.First(course => course.Id == courseId);
                var toAdd = new Homework() { 
                                             TaskId = taskId, 
                                             CourseId = courseId,
                                             Course = relatedCourse,
                                             Task = relatedTask
                                           };
                db.Homework.Add(toAdd);
                relatedCourse.Homework.Add(toAdd);
                db.SaveChanges();
            }
        }
        
        public static void DeleteHomeworkFromCourse(int hwId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.Homework.First(task => task.Id == hwId);
                db.Homework.Remove(toDelete);
                db.SaveChanges();
            }
        }

        public static IEnumerable<Homework> GetAllHwOnCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var hw = db.Homework.Where(task => task.CourseId == courseId);
                return hw;
            }
        }
    }
}