﻿using System.Linq;
using System.Runtime;
using DataManager.Models;

namespace DataManager
{
    public class OngoingCoursesManager
    {
        private static void GetRelatedHomework(int courseId)
        {
            
        }
        public static void AddNewOngoingCourse(int teacherId, int courseId, string group, string title)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedCourse = 
                    db.Course.First(c => c.Title == title);
                var teacher = db.Teacher.First(t => t.Id == teacherId);
                var course = db.Course.First(c => c.Id == courseId);
                var toAdd =
                    new OngoingCourse()
                    {
                        TeacherId = teacherId,
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
            foreach (var lec in relatedLec)
                LecturesManager.DeleteRelatedInfo(db, lec.Id);
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

        public static void AssignToCourse(int studentId, int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var targetSt = db.Student.Find(studentId);
                var targetCourse = db.OngoingCourse.Find(courseId);
                var toAdd = new StudentCourse()
                {
                    StudentId = studentId,
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