﻿using System.Collections.Generic;
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
                var target = db.Lecture.Find(lectureId);
                var toAdd = new Material() {
                                             LectureId = lectureId, 
                                             Url = url, 
                                             Lecture = target
                                           };
                db.Material.Add(toAdd);
                db.SaveChanges();
            }
        }

        public static void ChangeUrl(int id, string newUrl)
        {
            using (var db = new HwProj_DBContext())
            {
                var target = db.Material.Find(id);
                target.Url = newUrl;
                db.SaveChanges();
            }
        }
        
        public static Lecture GetRelatedLecture(int matId)
        {
            using (var db = new HwProj_DBContext())
            {
                var lec = db.Lecture.First(l => l.Material.Any(a => a.Id == matId));
                return lec;
            }
        }
        
        public static List<Material> GetRelatedMaterials(int lecId)
        {
            using (var db = new HwProj_DBContext())
            {
                var materials = db.Material.Where(a => a.LectureId == lecId).ToList();
                return materials;
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