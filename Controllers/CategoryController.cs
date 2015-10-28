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
    public class CategoryController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            HttpPostedFileBase file = Request.Files["imageUpload"];
            if (file != null && file.ContentLength > 0)
            {
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = file.FileName;
                file.SaveAs(Path.Combine(pathToSave, fileName));
                category.Thumbnail = "/Images/Uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,IsDelete")] Category category)
        {
            HttpPostedFileBase image = Request.Files["imageUpload"];
            Category cat = db.Categories.Find(category.CategoryId); 
            if (image != null && image.ContentLength> 0)
            {
                //xóa ảnh cũ
                string fullPath = Request.MapPath("~" + cat.Thumbnail);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //save ảnh mới
                string pathToSave = Server.MapPath("~/Images/Uploads");
                string fileName = image.FileName;
                image.SaveAs(Path.Combine(pathToSave, fileName));
                //change thumb cat into database
                cat.Thumbnail = "/Images/Uploads/" + fileName;
            }   
            if (ModelState.IsValid)
            {
                cat.CategoryName = category.CategoryName;
                cat.IsDelete = Convert.ToBoolean(category.IsDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            category.IsDelete = true;
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
