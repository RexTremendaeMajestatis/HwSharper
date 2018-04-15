using Microsoft.EntityFrameworkCore;
using DataManager.Models;
using System.Linq;

namespace DataManager
{
    public class UserRegistry 
    {
        public void Create(string login, string pass, string fullname, string email, bool isTeacher)
        {
            using (var db = new HwProj_DBContext())
            {
                var newUser = new Userdata();
                newUser.Username = login;
                newUser.Password = pass;
                newUser.Fullname = fullname;
                newUser.Email = email;
                newUser.IsTeacher = isTeacher;
                db.Userdata.Add(newUser);
                db.SaveChanges();
            }
        }
        public bool Search(string login, string pass) 
        {
            using (var db = new HwProj_DBContext())
            {
                var found = db.Userdata
                              .Where(user => user.Username == login && user.Password == pass)
                              .FirstOrDefault();
                return !object.Equals(default(Userdata), found);

            }
        }
    }
        
}
    