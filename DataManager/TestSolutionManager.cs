using System.Linq;
using DataManager.Models;
using System.Collections.Generic;

namespace DataManager
{
    public class TestSolutionManager
    {
        public static void AddSolution(string studentEmail, int testId, string url)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedStudent = db.Student.First(s => s.Email == studentEmail);
                var relatedTest = db.CurrentTest.First(test => test.Id == testId);
                var toAdd = new TestSolution() {
                                                 StudentId = studentEmail,
                                                 TestId = testId,
                                                 Url = url,
                                                 Test = relatedTest,
                                                 Student = relatedStudent
                                               };
                relatedStudent.TestSolution.Add(toAdd);
                relatedTest.TestSolution.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void SetNewSolution(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toSet = db.TestSolution.First(s => s.Id == id);
                toSet.Status = 1;
                db.SaveChanges();
            }
        }
        
        public static void SetRequestChanges(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toSet = db.TestSolution.First(s => s.Id == id);
                toSet.Status = 2;
                db.SaveChanges();
            }
        }
        
        public static void SetAccepted(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toSet = db.TestSolution.First(s => s.Id == id);
                toSet.Status = 3;
                db.SaveChanges();
            }
        }
        
        public static List<TestSolution> GetAllTestSolutions()
        {
            using (var db = new HwProj_DBContext())
            {
                var solutions = db.TestSolution.ToList();
                return solutions;
            }
        }
                       
        public static List<TestSolution> GetAllRelatedTestSolutions(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedSolutions = db.TestSolution.Where(test => test.Test.CourseId == courseId).ToList();
                return relatedSolutions;
            }
        }       

        public static void DeleteSolution(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.TestSolution.First(s => s.Id == id);
                db.TestSolution.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}