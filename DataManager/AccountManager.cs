using System.Collections.Generic;
using DataManager.Models;
using System.Linq;
using System.Timers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataManager
{

    public static class AccountManager
    {      
        
        private static IEnumerable<Teacher> GetTeachers(HwProj_DBContext db)
        {
            var teachers = db.Teacher.AsEnumerable();
            return teachers;
        }

        private static IEnumerable<Student> GetStudents(HwProj_DBContext db)
        {
            var students = db.Student.AsEnumerable();
            return students;
        }

        public static bool IsTeacher(string email)
        {
            using (var db = new HwProj_DBContext())
            {
                return GetTeachers(db).Any(user => user.Email == email);
            }
        }
        
        public static bool IsStudent(string email)
        {
            using (var db = new HwProj_DBContext())
            {
                return GetStudents(db).Any(user => user.Email == email);
            }
        }

        public static bool CreateAccount(string email, string pass, string fullname, bool isTeacher)
        {
            using (var db = new HwProj_DBContext())
            {
                if (db.Teacher.Any(user => user.Email == email) || db.Student.Any(user => user.Email == email))
                    return false;
                if (isTeacher)
                {
                    var newTeacher = new Teacher() {Email = email, Password = pass, Fullname = fullname};
                    db.Teacher.Add(newTeacher);
                }
                else
                {
                    var newStudent = new Student() {Email = email, Password = pass, Fullname = fullname};
                    db.Student.Add(newStudent);
                }
                db.SaveChanges();
                return true;
            }
        }

        public static bool ValidateUser(string email, string pass)
        {
            using (var db = new HwProj_DBContext())
            {
                var found = (GetTeachers(db).Any(user => user.Email == email && user.Password == pass) ||
                             GetStudents(db).Any(user => user.Email == email && user.Password == pass));
                return found;
            }
        }

        public static bool ChangePassword(string email, string curPass, string passRepeat, string newPass)
        {
            if (curPass != passRepeat)
                return false;
            using (var db = new HwProj_DBContext())
            {
                if (db.Teacher.Any(t => t.Email == email && t.Password == curPass))
                    db.Teacher.First(t => t.Email == email).Password = newPass;
                else if (db.Student.Any(t => t.Email == email && t.Password == curPass))
                    db.Student.First(s => s.Email == email).Password = newPass;
                else 
                    return false;
                db.SaveChanges();
                return true;
            }
        }
        
        public static bool ChangeEmail(string email, string curPass, string passRepeat, string newEmail)
        {
            if (curPass != passRepeat)
                return false;
            using (var db = new HwProj_DBContext())
            {
                if (db.Teacher.Any(t => t.Email == email && t.Password == curPass))
                    db.Teacher.First(t => t.Email == email).Password = newEmail;
                else if (db.Student.Any(t => t.Email == email && t.Password == curPass))
                    db.Student.First(s => s.Email == email).Password = newEmail;
                else 
                    return false;
                db.SaveChanges();
                return true;
            }
        }
        
        public static bool ChangeName(string email, string curPass, string passRepeat, string newName)
        {
            if (curPass != passRepeat)
                return false;
            using (var db = new HwProj_DBContext())
            {
                if (db.Teacher.Any(t => t.Email == email && t.Password == curPass))
                    db.Teacher.First(t => t.Email == email).Password = newName;
                else if (db.Student.Any(t => t.Email == email && t.Password == curPass))
                    db.Student.First(s => s.Email == email).Password = newName;
                else 
                    return false;
                db.SaveChanges();
                return true;
            }
        }

        //rewrite
        public static void DeleteRelatedInfo(HwProj_DBContext db, string email)
        {
            if (db.Teacher.Any(t => t.Email == email))
            {
                var relatedCourses = db.OngoingCourse.Where(c => c.TeacherId == email);
                var relatedAssign = db.StudentCourse.Where(a => a.Course.TeacherId == email);
                var relatedAnn = db.Announcement.Where(a => a.Lecture.Course.TeacherId == email);
                var relatedLec = db.Lecture.Where(l => l.Course.TeacherId == email);
                var relatecMat = db.Material.Where(l => l.Lecture.Course.TeacherId== email);
                db.OngoingCourse.RemoveRange(relatedCourses);
                db.StudentCourse.RemoveRange(relatedAssign);
                db.Announcement.RemoveRange(relatedAnn);
                db.Lecture.RemoveRange(relatedLec);
                db.Material.RemoveRange(relatecMat);
            }
            else
            {
                var relatedHwSolutions = db.HomeworkSolution.Where(hw => hw.StudentId == email);
                var relatedTestSolutions = db.TestSolution.Where(test => test.StudentId == email);
                var relatedAssign = db.StudentCourse.Where(a => a.StudentId == email);
                db.HomeworkSolution.RemoveRange(relatedHwSolutions);
                db.TestSolution.RemoveRange(relatedTestSolutions);
                db.StudentCourse.RemoveRange(relatedAssign);
            }
        }

        public static bool DeleteUser(string email, string passAssert, string passRepeat)
        {
            using (var db = new HwProj_DBContext())
            {
                if (passAssert != passRepeat)
                    return false;
                
                if (db.Teacher.Any(t => t.Email == email))
                {
                    var curUser = db.Teacher.Find(email);
                    DeleteRelatedInfo(db, email);
                    db.Teacher.Remove(curUser);
                }
                else
                {
                    var curUser = db.Student.Find(email);
                    DeleteRelatedInfo(db, email);
                    db.Student.Remove(curUser);
                }
                db.SaveChanges();
                return true;
            }
        }
        
    }
}
