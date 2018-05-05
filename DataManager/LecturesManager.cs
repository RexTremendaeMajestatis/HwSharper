using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public class LecturesManager
    {
        public static void AddNewLecture(int courseId, string title)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedCourse = db.OngoingCourse.First(c => c.Id == courseId);
                var toAdd = new Lecture()
                {
                    CourseId = courseId,
                    Title = title,
                    Course = relatedCourse
                };
                relatedCourse.Lecture.Add(toAdd);
                db.Lecture.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void DeleteLecture(int lecId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.Lecture.First(l => l.Id == lecId);
                db.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}