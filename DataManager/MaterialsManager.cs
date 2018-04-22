using System.Linq;
using DataManager.Models;

namespace DataManager
{
    public class MaterialsManager
    {
        public static void AddMaterial(int lectureId, string url)
        {
            using (var db = new HwProj_DBContext())
            {
                var target = db.Lecture.First(l => l.Id == lectureId);
                var toAdd = new Material() {LectureId = lectureId, Url = url, Lecture = target};
                target.Material.Add(toAdd);
                db.Material.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void DeleteMaterial(int id)
        {
            using (var db = new HwProj_DBContext())
            {
                var toDelete = db.Material.Find(id);
                db.Material.Remove(toDelete);
                db.SaveChanges();
            }
        }
    }
}