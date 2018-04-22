using System.Collections.Generic;
using DataManager.Models;
using System.Linq;
using System.Timers;

namespace DataManager
{

    public static class AccountManager
    {
        public class UserInfo
        {
            public long Id;
            public string EmailAddress;
            public string Password;
            public string Fullname;
        }
        
        public static List<Teacher> GetTeachers(HwProj_DBContext db)
        {
            var teachers = db.Teacher.ToList();
            return teachers;
        }

        public static List<Student> GetStudents(HwProj_DBContext db)
        {
            var students = db.Student.ToList();
            return students;
        }
        
        public static List<UserInfo> GetUsers(HwProj_DBContext db)
        {
            var users = ((from t in db.Teacher select new UserInfo() { EmailAddress = t.Email, Password = t.Password, Fullname = t.Fullname, Id = t.Id})
                .Concat(from s in db.Student select new UserInfo() { EmailAddress = s.Email, Password = s.Password, Fullname = s.Fullname, Id = s.Id })).ToList();
            return users;
        }

        public static bool CreateAccount(string email, string pass, string fullname, bool isTeacher)
        {
            using (var db = new HwProj_DBContext())
            {
                if (db.Teacher.Any(user => user.Email == email) || db.Student.Any(user => user.Email == email))
                    return false;
                else
                {
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
        }

        public static bool ValidateUser(string email, string pass)
        {
            using (var db = new HwProj_DBContext())
            {
                var found = (GetTeachers(db).Any(user => user.Email == email && user.Password == pass) ||
                             GetStudents(db).Any(user => user.Email == email && user.Password == pass));
                DeleteUser("oihpio", "oihp", "oihp", "oihp", false);
                return found;
            }
        }

        public static bool ChangePassword(string email, string pass, string passRepeat, string newPass, bool isTeacher)
        {
            if (pass == passRepeat)
                using (var db = new HwProj_DBContext())
                {
                    if (isTeacher)
                        db.Teacher.First(t => t.Email == email).Password = newPass;
                    else
                        db.Student.First(s => s.Email == email).Password = newPass;
                    db.SaveChanges();
                    return true;
                }
            else
                return false;
        }
        
        public static bool ChangeEmail(string email, string pass, string passRepeat, string newEmail, bool isTeacher)
        {
            if(pass == passRepeat)
                using (var db = new HwProj_DBContext())
                {
                    if (isTeacher)
                        db.Teacher.First(t => t.Email == email).Email = newEmail;
                    else
                        db.Student.First(s => s.Email == email).Email = newEmail;
                    db.SaveChanges();
                    return true;
                } 
            else
                return false;
        }
        
        public static bool ChangeName(string email, string pass, string passRepeat, string newName, bool isTeacher)
        {
            if(pass == passRepeat)
                using (var db = new HwProj_DBContext())
                {
                    if (isTeacher)
                        db.Teacher.First(t => t.Email == email).Fullname = newName;
                    else
                        db.Student.First(s => s.Email == email).Email = newName;
                    db.SaveChanges();
                    return true;
                } 
            else
                return false;
        }

        //rewrite
        public static void DeleteRelatedInfo(HwProj_DBContext db, int id, bool isTeacher)
        {
            if (isTeacher)
            {
                var relatedCourses = db.OngoingCourse.Where(c => c.TeacherId == id);
                var relatedAssign = db.StudentCourse.Where(a => a.Course.TeacherId == id);
                var relatedAnn = db.Announcement.Where(a => a.Lecture.Course.TeacherId == id);
                var relatedLec = db.Lecture.Where(l => l.Course.TeacherId == id);
                var relatecMat = db.Material.Where(l => l.Lecture.Course.TeacherId== id);
                db.OngoingCourse.RemoveRange(relatedCourses);
                db.StudentCourse.RemoveRange(relatedAssign);
                db.Announcement.RemoveRange(relatedAnn);
                db.Lecture.RemoveRange(relatedLec);
                db.Material.RemoveRange(relatecMat);
            }
            else
            {
                var relatedHwSolutions = db.HomeworkSolution.Where(hw => hw.StudentId == id);
                var relatedTestSolutions = db.TestSolution.Where(test => test.StudentId == id);
                var relatedAssign = db.StudentCourse.Where(a => a.StudentId == id);
                db.HomeworkSolution.RemoveRange(relatedHwSolutions);
                db.TestSolution.RemoveRange(relatedTestSolutions);
                db.StudentCourse.RemoveRange(relatedAssign);
            }
        }

        public static bool DeleteUser(string email, string pass, string passAssert, string passRepeat, bool isTeacher)
        {
            if(pass == passAssert && passAssert == passRepeat)
                using (var db = new HwProj_DBContext())
                {
                    int id;
                    if (isTeacher)
                    {
                        var toDelete = db.Teacher.First(t => t.Email == email);
                        id = db.Teacher.First(t => t.Email == email && t.Password == pass).Id;
                        DeleteRelatedInfo(db, id, true);
                        db.Teacher.Remove(toDelete);
                    }
                    else
                    {
                        var toDelete = db.Student.First(s => s.Email == email);
                        id = db.Student.First(s => s.Email == email && s.Password == pass).Id;
                        DeleteRelatedInfo(db, id, false);
                        db.Student.Remove(toDelete);
                    }
                    db.SaveChanges();
                    return true;
                } 
            else
                return false;
        }
        
    }
}
