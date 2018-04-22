using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public class HwSolutionManager
    {
        public static void AddSolution(int studentId, int hwId, string url)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedStudent = db.Student.First(s => s.Id == studentId);
                var relatedHw = db.Homework.First(hw => hw.Id == hwId);
                var toAdd = new HomeworkSolution()
                {
                    StudentId = studentId,
                    HomeworkId = hwId,
                    Url = url,
                    Homework = relatedHw,
                    Student = relatedStudent
                };
                relatedStudent.HomeworkSolution.Add(toAdd);
                relatedHw.HomeworkSolution.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void SetNewSolution(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toSet = db.HomeworkSolution.First(s => s.Id == id);
                toSet.Status = 1;
                db.SaveChanges();
            }
        }
        
        public static void SetRequestChanges(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toSet = db.HomeworkSolution.First(s => s.Id == id);
                toSet.Status = 2;
                db.SaveChanges();
            }
        }
        
        public static void SetAccepted(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toSet = db.HomeworkSolution.First(s => s.Id == id);
                toSet.Status = 3;
                db.SaveChanges();
            }
        }

        public static void DeleteSolution(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.HomeworkSolution.First(s => s.Id == id);
                db.HomeworkSolution.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}