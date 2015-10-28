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
    public class SteptoCreateController : Controller
    {
        private DBContext db = new DBContext();

        // GET: SteptoCreate
        public ActionResult Index()
        {
            return View(db.SteptoCreates.ToList());
        }

        // GET: SteptoCreate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SteptoCreate steptoCreate = db.SteptoCreates.Find(id);
            if (steptoCreate == null)
            {
                return HttpNotFound();
            }
            return View(steptoCreate);
        }

        // GET: SteptoCreate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SteptoCreate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Steps,Slogan,ThumbnailPath")] SteptoCreate steptoCreate)
        {
            HttpPostedFileBase file = Request.Files["imageUpload"];
            if (file != null && file.ContentLength > 0)
            {
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = file.FileName;
                file.SaveAs(Path.Combine(pathToSave, fileName));
                steptoCreate.ThumbnailPath = "/Images/Uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                db.SteptoCreates.Add(steptoCreate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(steptoCreate);
        }

        // GET: SteptoCreate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SteptoCreate steptoCreate = db.SteptoCreates.Find(id);
            if (steptoCreate == null)
            {
                return HttpNotFound();
            }
            return View(steptoCreate);
        }

        // POST: SteptoCreate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Title,Steps,Slogan,ThumbnailPath,IsDelete")] SteptoCreate steptoCreate)
        {
            HttpPostedFileBase file = Request.Files["imageUpload"];
            if (file != null && file.ContentLength > 0)
            {
                //xoa ảnh củ
                string fullPath = Request.MapPath("~" + steptoCreate.ThumbnailPath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = file.FileName;
                file.SaveAs(Path.Combine(pathToSave, fileName));
                steptoCreate.ThumbnailPath = "/Images/Uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(steptoCreate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(steptoCreate);
        }

        // GET: SteptoCreate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SteptoCreate steptoCreate = db.SteptoCreates.Find(id);
            if (steptoCreate == null)
            {
                return HttpNotFound();
            }
            return View(steptoCreate);
        }

        // POST: SteptoCreate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SteptoCreate steptoCreate = db.SteptoCreates.Find(id);
            db.SteptoCreates.Remove(steptoCreate);
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
