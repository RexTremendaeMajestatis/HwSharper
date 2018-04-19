using System.Collections.Generic;
using DataManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace DataManager
{
    public class UserInfo
    {
        public int Id;
        public string EmailAddress;
        public string Password;
        public string Fullname;
    }
    public static class AccountManager
    {
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
            var users = (from t in db.Teacher select new UserInfo() { EmailAddress = t.Email, Password = t.Password })
                .Concat(from s in db.Student select new UserInfo() { EmailAddress = s.Email, Password = s.Password }).ToList();
            return users;
        }

        public static bool CreateAccount(string email, string pass, string fullname, bool isTeacher)
        {
            using (var db = new HwProj_DBContext())
            {
                if (GetUsers(db).Any(user => user.EmailAddress == email))
                    return false;
                else
                {
                    var x = GetUsers(db);
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
                return GetUsers(db).Any(user => (user.EmailAddress == email) && (user.Password == pass));
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

        public static bool DeleteUser(string email, string pass, string passAssert, string passRepeat, bool isTeacher)
        {
            if(pass == passAssert && passAssert == passRepeat)
                using (var db = new HwProj_DBContext())
                {
                    if (isTeacher)
                    {
                        var toDelete = db.Teacher.First(t => t.Email == email);
                        db.Teacher.Remove(toDelete);
                    }
                    else
                    {
                        var toDelete = db.Student.First(s => s.Email == email);
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
