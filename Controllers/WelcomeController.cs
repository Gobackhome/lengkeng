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
    public class WelcomeController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Welcome
        public ActionResult Index()
        {
            return View(db.Welcomes.ToList());
        }

        // GET: Welcome/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Welcome welcome = db.Welcomes.Find(id);
            if (welcome == null)
            {
                return HttpNotFound();
            }
            return View(welcome);
        }

        // GET: Welcome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Welcome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,LinkVideo,IsDelete")] Welcome welcome, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                //HttpPostedFileBase file = Request.Files["imageUpload"];
                if (imageUpload != null && imageUpload.ContentLength > 0)
                {
                    string pathToSave = Server.MapPath("~/Images/Uploads");
                    string fileName = imageUpload.FileName;
                    imageUpload.SaveAs(Path.Combine(pathToSave, fileName));
                    welcome.Background = "/Images/Uploads/" + fileName;
                }
                else
                {
                    welcome.Background = "";
                }
                db.Welcomes.Add(welcome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(welcome);
        }

        // GET: Welcome/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Welcome welcome = db.Welcomes.Find(id);
            if (welcome == null)
            {
                return HttpNotFound();
            }
            return View(welcome);
        }

        // POST: Welcome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,LinkVideo,IsDelete")] Welcome welcome)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["imageUpload"];
                if (file != null && file.ContentLength > 0)
                {
                    string pathToSave = Server.MapPath("~/Images/Uploads");
                    string fileName = file.FileName;
                    file.SaveAs(Path.Combine(pathToSave, fileName));
                    welcome.Background = "/Images/Uploads/" + fileName;
                }
                db.Entry(welcome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(welcome);
        }

        // GET: Welcome/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Welcome welcome = db.Welcomes.Find(id);
            if (welcome == null)
            {
                return HttpNotFound();
            }
            return View(welcome);
        }

        // POST: Welcome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Welcome welcome = db.Welcomes.Find(id);
            db.Welcomes.Remove(welcome);
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
