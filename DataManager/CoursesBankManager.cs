﻿using System.Collections.Generic;
using System.Linq;
using DataManager.Models;
namespace DataManager
{
    public class CoursesBankManager
    {      
        public static bool AddNewCourse(string title)
        {
            using (var db = new HwProj_DBContext())
            {
                if (db.Course.Any(course => course.Title == title))
                    return false;
                else
                {
                    db.Course.Add(new Course() {Title = title});                  
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public static void ChangeCourseTitle(int courseId, string newTitle)
        {
            using (var db = new HwProj_DBContext())
            {
                var target = db.Course.Find(courseId);
                target.Title = newTitle;
                db.SaveChanges();
            }
        }

        public static List<Course> GetAllCourses()
        {
            using (var db = new HwProj_DBContext())
            {
                var courses = db.Course.ToList();
                return courses;
            }
        }
        
        public static void DeleteCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var courseToDelete = db.Course.Find(courseId);
                db.Course.Remove(courseToDelete);
                db.SaveChanges();
            }
        }
    }
}