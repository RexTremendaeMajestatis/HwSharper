using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Collections.Generic;
using DataManager.Models;

namespace DataManager
{
    public class OngoingCoursesManager
    {
        private static void GetRelatedHomework(int courseId)
        {
            
        }
        public static void AddNewOngoingCourse(string teacherEmail, int courseId, string group, string title)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedCourse = 
                    db.Course.First(c => c.Title == title);
                var teacher = db.Teacher.First(t => t.Email == teacherEmail);
                var course = db.Course.First(c => c.Id == courseId);
                var toAdd =
                    new OngoingCourse()
                    {
                        TeacherId = teacherEmail,
                        CourseId = courseId,
                        GroupId = group,
                        Course = course,
                        Teacher = teacher,
                    };
                db.OngoingCourse.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void DeleteRelatedInfo(HwProj_DBContext db, int id)
        {
            var course = db.OngoingCourse.First(c => c.Id == id);
            var relatedLec = db.Lecture.Where(l => l.CourseId == id);
            var relatedAssign = db.StudentCourse.Where(sc => sc.CourseId == id);
            db.StudentCourse.RemoveRange(relatedAssign);
            
        }

        public static void DeleteOngoingCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.OngoingCourse.First(course => course.Id == courseId);
                DeleteRelatedInfo(db, courseId);
                db.OngoingCourse.Remove(toDelete);
                db.SaveChanges();
            }
        }

        public static void AssignToCourse(string studentEmail, int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var targetSt = db.Student.Find(studentEmail);
                var targetCourse = db.OngoingCourse.Find(courseId);
                var toAdd = new StudentCourse()
                {
                    StudentId = studentEmail,
                    CourseId = courseId,
                    Course = targetCourse,
                    Student = targetSt
                };
                targetSt.StudentCourse.Add(toAdd);
                targetCourse.StudentCourse.Add(toAdd);
                db.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static IEnumerable<Student> GetAllStudentsOnCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
               var students = db.Student.Where(s => s.StudentCourse.Any(sc => sc.CourseId == courseId)).AsEnumerable();
               return students;
            }
        }

        public static void LeaveCourse(int studentId, int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.StudentCourse.Find((studentId, courseId));
                db.StudentCourse.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}