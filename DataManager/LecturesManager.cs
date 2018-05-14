using System.Collections.Generic;
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
                var toAdd = new Lecture() {
                                            CourseId = courseId,
                                            Title = title,
                                            Course = relatedCourse
                                          };
                relatedCourse.Lecture.Add(toAdd);
                db.Lecture.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void ChangeLectureTitle(int lecId, string newTitle)
        {
            using (var db = new HwProj_DBContext())
            {
                var target = db.Lecture.Find(lecId);
                target.Title = newTitle;
                db.SaveChanges();
            }
        }

        public static List<Lecture> GetAllLectures()
        {
            using (var db = new HwProj_DBContext())
            {
                var lectures = db.Lecture.ToList();
                return lectures;
            }
        }

        public static List<Lecture> GetRelatedLectures(int courseId)
        {
            using (var db = new HwProj_DBContext())
            {
                var relatedLectures = db.Lecture.Where(lec => lec.CourseId == courseId).ToList();
                return relatedLectures;
            }
        }

        public static void DeleteLecture(int lecId)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.Lecture.Find(lecId);
                db.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}