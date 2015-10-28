using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lengkeng.DataAccessLayer;
using lengkeng.Models;

namespace lengkeng.Controllers
{
    public class YoutubeController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Youtube
        public ActionResult Index()
        {
            return View(db.YoutubeHomes.ToList());
        }

        // GET: Youtube/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YoutubeHome youtubeHome = db.YoutubeHomes.Find(id);
            if (youtubeHome == null)
            {
                return HttpNotFound();
            }
            return View(youtubeHome);
        }

        // GET: Youtube/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Youtube/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,YoutubeId1,TitleYoutubeId1,YoutubeId2,TitleYoutubeId2,IsDelete")] YoutubeHome youtubeHome)
        {
            HttpPostedFileBase file = Request.Files["imageUpload"];
            if (file != null && file.ContentLength > 0)
            {
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = file.FileName;
                file.SaveAs(Path.Combine(pathToSave, fileName));
                youtubeHome.Thumb = "/Images/Uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                db.YoutubeHomes.Add(youtubeHome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(youtubeHome);
        }

        // GET: Youtube/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YoutubeHome youtubeHome = db.YoutubeHomes.Find(id);
            if (youtubeHome == null)
            {
                return HttpNotFound();
            }
            return View(youtubeHome);
        }

        // POST: Youtube/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,YoutubeId1,TitleYoutubeId1,YoutubeId2,TitleYoutubeId2,Thumb,IsDelete")] YoutubeHome youtubeHome, string hf_ThumbnailPath)
        {
            HttpPostedFileBase file = Request.Files["imageUpload"];
            if (file != null && file.ContentLength > 0)
            {
                //Xóa ảnh cũ
                string fullPath = Request.MapPath("~" + hf_ThumbnailPath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //lưu ảnh mới
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = file.FileName;
                file.SaveAs(Path.Combine(pathToSave, fileName));
                youtubeHome.Thumb = "/Images/Uploads/" + fileName;
            }
            else
            {

                youtubeHome.Thumb = hf_ThumbnailPath;
            }
            if (ModelState.IsValid)
            {
                db.Entry(youtubeHome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(youtubeHome);
        }

        // GET: Youtube/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YoutubeHome youtubeHome = db.YoutubeHomes.Find(id);
            if (youtubeHome == null)
            {
                return HttpNotFound();
            }
            return View(youtubeHome);
        }

        // POST: Youtube/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YoutubeHome youtubeHome = db.YoutubeHomes.Find(id);
            db.YoutubeHomes.Remove(youtubeHome);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
