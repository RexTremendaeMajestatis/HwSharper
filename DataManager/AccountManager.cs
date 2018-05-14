using System.Collections.Generic;
using DataManager.Models;
using System.Linq;

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
                    var newTeacher = new Teacher() {Email = email, Password = pass, FullName = fullname};
                    db.Teacher.Add(newTeacher);
                }
                else
                {
                    var newStudent = new Student() {Email = email, Password = pass, FullName = fullname};
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
                //db.SaveChanges();
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

        public static bool DeleteUser(string email, string passAssert, string passRepeat)
        {
            using (var db = new HwProj_DBContext())
            {
                if (passAssert != passRepeat)
                    return false;
                
                if (db.Teacher.Any(t => t.Email == email && t.Password == passAssert))
                {
                    var curUser = db.Teacher.Find(email);
                    db.Teacher.Remove(curUser);
                }
                else if (db.Student.Any(s => s.Email == email && s.Password == passAssert))
                {
                    var curUser = db.Student.Find(email);
                    db.Student.Remove(curUser);
                }
                else
                    return false;
                db.SaveChanges();
                return true;
            }
        }
        
    }
}
