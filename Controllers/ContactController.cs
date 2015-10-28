using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lengkeng.DataAccessLayer;
using lengkeng.Models;

namespace lengkeng.Controllers
{
    public class ContactController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Contact
        public ActionResult Index()
        {
            List<Contact> listContact = db.Contacts.OrderByDescending(c => c.IsDelete).ToList();
            string timestemp;
            ContactBusinessLayer contactBusinessLayer = new ContactBusinessLayer();
            List<Contact> listContactIndex = new List<Contact>();
            foreach (var item in listContact)
            {
                timestemp = contactBusinessLayer.getTime(item.TimeStartEnd);
                if (timestemp != null)
                {
                    item.TimeStartEnd = timestemp;
                    listContactIndex.Add(item);
                }
            }
            return View(listContactIndex);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ContactBusinessLayer contactBusinessLayer = new ContactBusinessLayer();
            string timestemp = contactBusinessLayer.getTime(contact.TimeStartEnd);
            if (timestemp != null)
            {
                contact.TimeStartEnd = timestemp;
            }
            return View(contact);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string time26Start, string time26End, string timeCNStart, string timeCNEnd, Contact contact)
        {
            contact.TimeStartEnd = time26Start + "," + time26End + "," + timeCNStart + "," + timeCNEnd;
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ContactBusinessLayer contactBusinessLayer = new ContactBusinessLayer();
            ViewData["timestemp"] = contactBusinessLayer.getTimeArr(contact.TimeStartEnd);

            return View(contact);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string time26Start, string time26End, string timeCNStart, string timeCNEnd, Contact contact)
        {
            contact.TimeStartEnd = time26Start + "," + time26End + "," + timeCNStart + "," + timeCNEnd;
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            contact.IsDelete = true;
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
