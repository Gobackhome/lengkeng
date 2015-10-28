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
using lengkeng.Filters;
using lengkeng.Models;

namespace lengkeng.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: News
        public ActionResult Index()
        {
            return View(db.Newss.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.Newss.Find(id);
            Account account = db.Accounts.Find(news.UserId);
            ViewBag.UserFullName = account.Fullname;
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            ViewData["Account"] = db.Accounts.OrderByDescending(a => a.Id).ToList();
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,UserId")] News news)
        {
            HttpPostedFileBase thumb = Request.Files["imageUpload"];
            if (ModelState.IsValid)
            {
                if (thumb != null && thumb.ContentLength > 0)
                {
                    string pathToSave = Server.MapPath("~/Images/Uploads");
                    string fileName = thumb.FileName;
                    thumb.SaveAs(Path.Combine(pathToSave, fileName));
                    news.Thumb = "/Images/Uploads/" + fileName;
                }
                news.DatePost = DateTime.Now;

                db.Newss.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.Newss.Find(id);
            ViewData["Account"] = db.Accounts.Where(a => a.IsDelete != true).OrderByDescending(a => a.Id).ToList();
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,DatePost,UserId,IsDelete")] News news, string hf_ThumbnailPath,string hf_DatePost)
        {
            HttpPostedFileBase thumb = Request.Files["imageUpload"];
            //edit thumbnail
            //Nếu có chọn ảnh đại diện mới
            if (ModelState.IsValid)
            {
                if (thumb != null && thumb.ContentLength > 0)
                {
                    //Xóa ảnh cũ
                    string fullPath = Request.MapPath("~" + hf_ThumbnailPath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    //lưu ảnh mới
                    string pathToSave = Server.MapPath("~/Images/Uploads");
                    string fileName = thumb.FileName;
                    thumb.SaveAs(Path.Combine(pathToSave, fileName));
                    news.Thumb = "/Images/Uploads/" + fileName;
                }
                else
                {

                    news.Thumb = hf_ThumbnailPath;
                }
                news.DatePost = DateTime.Parse(hf_DatePost);
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.Newss.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.Newss.Find(id);
            news.IsDelete = true;
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
