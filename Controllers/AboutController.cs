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
    
    public class AboutController : Controller
    {
        private DBContext db = new DBContext();


        // GET: About
        public ActionResult Index()
        {
            return View(db.Abouts.ToList());
        }

        // GET: About/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }
        [Authorize]
        // GET: About/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: About/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,content,Thumbnails,IsDelete")] About about)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    string pathToSave = Server.MapPath("~/Images/Uploads");
                    string fileName = file.FileName;
                    file.SaveAs(Path.Combine(pathToSave, fileName));
                    about.Thumbnails = "/Images/Uploads/" + fileName;
                }
            }
            if (ModelState.IsValid)
            {
                db.Abouts.Add(about);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(about);
        }

        // GET: About/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: About/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,content,Thumbnails,IsDelete")] About about)
        {
            HttpPostedFileBase image = Request.Files["imageUpload"];
            About abo = db.Abouts.Find(about.Id);
            if (image != null && image.ContentLength > 0)
            {
                //xóa ảnh cũ
                string fullPath = Request.MapPath("~" + abo.Thumbnails);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //save ảnh mới
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = image.FileName;
                image.SaveAs(Path.Combine(pathToSave, fileName));
                //change thumb cat into database
                abo.Thumbnails = "/Images/Uploads/" + fileName;
            }

            if (ModelState.IsValid)
            {
                abo.Title = about.Title;
                abo.content = about.content;
                abo.IsDelete = Convert.ToBoolean(abo.IsDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }

        // GET: About/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: About/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            About about = db.Abouts.Find(id);
            about.IsDelete = true;
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
