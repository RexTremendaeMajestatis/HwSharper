﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public class TestManager
    {
        public static void AddTest(int taskId, int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedTask = db.TestTask.First(task => task.Id == taskId);
                var relatedCourse = db.Course.First(course => course.Id == courseId);
                var toAdd = new Test() { TaskId = taskId, 
                                         CourseId = courseId,
                                         Course = relatedCourse,
                                         Task = relatedTask
                                       };
                db.Test.Add(toAdd);
                relatedCourse.Test.Append(toAdd);
                db.SaveChanges();
            }
        }
        
        public static void DeleteRelatedInfo(HwProj_DBContext db, int id)
        {
            var relatedSolutions = db.TestSolution.Where(s => s.Test.TaskId == id);
            db.TestSolution.RemoveRange(relatedSolutions);
        }
             
        public static void DeleteTest(int testId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.Test.First(task => task.Id == testId);
                DeleteRelatedInfo(db, testId);
                db.Test.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}