using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public class AnnouncementsManager
    {
        public static void AddAnnouncement(int lectureId, string msg)
        {
            using (var db = new HwProj_DBContext())
            {
                var target = db.Lecture.First(l => l.Id == lectureId);
                var toAdd = new Announcement() {LectureId = lectureId, Message = msg, Lecture = target};
                target.Announcement.Add(toAdd);
                db.Announcement.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void ChangeAnnouncement(int id, string newMsg)
        {
            using (var db = new HwProj_DBContext())
            {
                var target = db.Announcement.Find(id);
                target.Message = newMsg;
                db.SaveChanges();
            }
        }

        public static void DeleteAnnouncement(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.Announcement.Find(id);
                db.Announcement.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}