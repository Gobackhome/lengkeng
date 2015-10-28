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
    public class FeatureController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Feature
        public ActionResult Index()
        {
            return View(db.Features.ToList());
        }

        // GET: Feature/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        // GET: Feature/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feature/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,IsOnlyImage")] Feature feature)
        {
            HttpPostedFileBase BackgroundImage = Request.Files["BackgroundImage"];
            HttpPostedFileBase ThumbnailPath1 = Request.Files["ThumbnailPath1"];
            HttpPostedFileBase ThumbnailPath2 = Request.Files["ThumbnailPath2"];
            //Ảnh nền
            if (BackgroundImage != null && BackgroundImage.ContentLength > 0)
            {
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = BackgroundImage.FileName;
                BackgroundImage.SaveAs(Path.Combine(pathToSave, fileName));
                feature.BackgroundImage = "/Images/Uploads/" + fileName;
            }
            //Ảnh phụ 1
            if (ThumbnailPath1 != null && ThumbnailPath1.ContentLength > 0)
            {
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = ThumbnailPath1.FileName;
                ThumbnailPath1.SaveAs(Path.Combine(pathToSave, fileName));
                feature.ThumbnailPath1 = "/Images/Uploads/" + fileName;
            }
            //Ảnh phụ 2
            if (ThumbnailPath2 != null && ThumbnailPath2.ContentLength > 0)
            {
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = ThumbnailPath1.FileName;
                ThumbnailPath2.SaveAs(Path.Combine(pathToSave, fileName));
                feature.ThumbnailPath2 = "/Images/Uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                db.Features.Add(feature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feature);
        }

        // GET: Feature/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        // POST: Feature/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,IsOnlyImage,IsDelete")] Feature feature)
        {
            Feature fea = db.Features.Find(feature.Id);
            HttpPostedFileBase BackgroundImage = Request.Files["BackgroundImage"];
            HttpPostedFileBase ThumbnailPath1 = Request.Files["ThumbnailPath1"];
            HttpPostedFileBase ThumbnailPath2 = Request.Files["ThumbnailPath2"];
            //Ảnh nền
            if (BackgroundImage != null && BackgroundImage.ContentLength > 0)
            {
                //xóa ảnh cũ
                string fullPath = Request.MapPath("~" + fea.BackgroundImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = BackgroundImage.FileName;
                BackgroundImage.SaveAs(Path.Combine(pathToSave, fileName));
                fea.BackgroundImage = "/Images/Uploads/" + fileName;
            }
            //Ảnh phụ 1
            if (ThumbnailPath1 != null && ThumbnailPath1.ContentLength > 0)
            {
                //xóa ảnh cũ
                string fullPath = Request.MapPath("~" + fea.ThumbnailPath1);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = ThumbnailPath1.FileName;
                ThumbnailPath1.SaveAs(Path.Combine(pathToSave, fileName));
                fea.ThumbnailPath1 = "/Images/Uploads/" + fileName;
            }
            //Ảnh phụ 2
            if (ThumbnailPath2 != null && ThumbnailPath2.ContentLength > 0)
            {
                //xóa ảnh cũ
                string fullPath = Request.MapPath("~" + fea.ThumbnailPath2);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = ThumbnailPath1.FileName;
                ThumbnailPath2.SaveAs(Path.Combine(pathToSave, fileName));
                fea.ThumbnailPath2 = "/Images/Uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                fea.Title = feature.Title;
                fea.IsOnlyImage = feature.IsOnlyImage;
                fea.Description = feature.Description;
                fea.IsDelete = Convert.ToBoolean(feature.IsDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feature);
        }

        // GET: Feature/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        // POST: Feature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feature feature = db.Features.Find(id);
            feature.IsDelete = true;
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
