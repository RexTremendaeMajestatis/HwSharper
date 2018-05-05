using System.Linq;
using System.Collections.Generic;
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
        
        public static void DeleteCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
            var courseToDelete =
                    db.Course.Find(courseId);
            db.Course.Remove(courseToDelete);
            db.SaveChanges();
            }
        }
    }
}