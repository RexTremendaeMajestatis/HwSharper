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
        
        //rewrite somehow
        public static void DeleteRelatedInfo(HwProj_DBContext db, int id)
        {
            var relatedAssign = db.StudentCourse.Where(a => a.CourseId == id);
            var relatedAnn = db.Announcement.Where(a => a.Lecture.CourseId == id);
            var relatedLec = db.Lecture.Where(l => l.CourseId == id);
            var relatecMat = db.Material.Where(l => l.Lecture.CourseId== id);
            var relatedCourses = db.OngoingCourse.Where(c => c.CourseId == id);
            var relatedHw = db.Homework.Where(h => h.CourseId == id);
            var relatedTests = db.Test.Where(h => h.CourseId == id);
            var relatedHwSolutions = db.HomeworkSolution.Where(s => s.Homework.CourseId == id);
            var relatedTestSolutions = db.TestSolution.Where(s => s.Test.CourseId == id);
            db.StudentCourse.RemoveRange(relatedAssign);
            db.Announcement.RemoveRange(relatedAnn);
            db.Lecture.RemoveRange(relatedLec);
            db.Material.RemoveRange(relatecMat);
            db.OngoingCourse.RemoveRange(relatedCourses);
            db.HomeworkSolution.RemoveRange(relatedHwSolutions);
            db.TestSolution.RemoveRange(relatedTestSolutions);
            db.Homework.RemoveRange(relatedHw);
            db.Test.RemoveRange(relatedTests);
        }
        
        public static void DeleteCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
            var courseToDelete =
                    db.Course.Find(courseId);
            DeleteRelatedInfo(db, courseId);
            db.Course.Remove(courseToDelete);
            db.SaveChanges();
            }
        }
    }
}