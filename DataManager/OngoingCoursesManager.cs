using System;
using System.Linq;
using System.Collections.Generic;
using DataManager.Models;

namespace DataManager
{
    public class OngoingCoursesManager
    {
        public static void AddNewOngoingCourse(string teacherEmail, int courseId, string group, string title)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedCourse = 
                    db.Course.First(c => c.Title == title);
                var teacher = db.Teacher.First(t => t.Email == teacherEmail);
                var course = db.Course.First(c => c.Id == courseId);
                var toAdd = new OngoingCourse() {
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

        public static List<OngoingCourse> GetAllOngoingCourses()
        {
            using (var db = new HwProj_DBContext())
            {
                var courses = db.OngoingCourse.ToList();
                return courses;
            }
        }

        public static List<OngoingCourse> GetAllTeachersOngoingCourses(string teacherEmail)
        {
            using (var db = new HwProj_DBContext())
            {
                var courses = db.OngoingCourse.Where(c => c.TeacherId == teacherEmail).ToList();
                return courses;
            }
        }

        public static void DeleteOngoingCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.OngoingCourse.First(course => course.Id == courseId);
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
                var toAdd = new StudentCourse() { 
                                                  StudentId = studentEmail,
                                                  CourseId = courseId,
                                                  Course = targetCourse,
                                                  Student = targetSt
                                                };
                db.StudentCourse.Add(toAdd);
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

        public static IEnumerable<Student> GetAllStudentsOnCourse(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var students = db.StudentCourse.Where(sc => sc.CourseId == courseId)
                                               .Select(sc => sc.Student).AsEnumerable();
               return students;
            }
        }
        
        public static void AssignHomework(int courseId, int hwId)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedHw = db.Homework.First(task => task.Id == hwId);
                var relatedCourse = db.OngoingCourse.First(course => course.Id == courseId);
                var curDate = DateTime.Today;
                var toAdd = new CurrentHomework() { 
                                                    CourseId = courseId,
                                                    HwId = hwId,
                                                    Date = curDate,
                                                    Course = relatedCourse,
                                                    Hw = relatedHw
                                                  };
                db.CurrentHomework.Add(toAdd);
                db.SaveChanges();
            }
        }
        
        public static void AssignTest(int courseId, int testId)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedTest = db.Test.First(task => task.Id == testId);
                var relatedCourse = db.OngoingCourse.First(course => course.Id == courseId);
                var curDate = DateTime.Today;
                var toAdd = new CurrentTest() { 
                                                CourseId = courseId,
                                                TestId = testId,
                                                Date = curDate,
                                                Course = relatedCourse,
                                                Test = relatedTest
                                              };
                db.CurrentTest.Add(toAdd);
                db.SaveChanges();
            }
        }
        
        public static List<Hometask> GetAssignedHomeworks(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var assignedHw = db.CurrentHomework.Where(hw => hw.CourseId == courseId)
                                                   .Select(hw => hw.Hw.Task).AsEnumerable().ToList();
                return assignedHw;
            }
        }
        
        public static List<TestTask> GetAssignedTests(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var assignedTests = db.CurrentTest.Where(test => test.CourseId == courseId)
                                                  .Select(test => test.Test.Task).AsEnumerable().ToList();
                return assignedTests;
            }
        }
    }
}