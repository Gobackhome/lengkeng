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
using lengkeng.ViewModels;

namespace lengkeng.Controllers
{
    public class ItemController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Items
        public ActionResult Index()
        {
            ItemAdminViewModel myItemListView = new ItemAdminViewModel();
            myItemListView.MyItemListView = new List<ItemFollowCategoryViewModel>();
            ItemFollowCategoryViewModel itemFollowCategoryViewModel = new ItemFollowCategoryViewModel();
            List<ItemViewModel> itemViewModels = new List<ItemViewModel>();

            var query = from cat in db.Categories
                        join item in db.Items.Where(i => i.IsDelete != true) on cat.CategoryId equals item.CategoryId
                        select new { cat.CategoryId, cat.CategoryName, cat.Thumbnail, item.ItemId, item.ItemName, item.ItemInfo, item.ItemStatus,item.IsActive, item.Prices, item.Promotions, item.ThumbnailPath };
            int sumCat = 0;
            int cateIDRoot = 0;
            bool isNewCat = false;
            int totalCat = query.ToList().Count();
            foreach (var item in query)
            {
                sumCat++;
                if (cateIDRoot == 0)
                {
                    isNewCat = true;
                }
                else if (cateIDRoot != item.CategoryId)
                {//neu catID lan loop nay khac voi goc va != 0, => la 1 nhom item moi
                    //add ItemViewModels vao itemFollowCategoryViewModel, itemFollowCategoryViewModel gio co du gia tri can roi, tiep tuc add no vao (list) itemFollowCategoryViewModels
                    //reset lai itemViewModels = null;
                    //reset lai itemFollowCategoryViewModel
                    //gan 1 flag = true de itemFollowCategoryViewModel them info cua cat vao
                    itemFollowCategoryViewModel.listItemViewModel = itemViewModels;
                    myItemListView.MyItemListView.Add(itemFollowCategoryViewModel);
                    itemViewModels = new List<ItemViewModel>();
                    itemFollowCategoryViewModel = new ItemFollowCategoryViewModel();
                    isNewCat = true;
                }
                ItemViewModel itemViewModel = new ItemViewModel();
                itemViewModel.ItemId = item.ItemId;
                itemViewModel.ItemName = item.ItemName;
                itemViewModel.ItemInfo = item.ItemInfo;
                itemViewModel.ThumbnailPath = item.ThumbnailPath;
                itemViewModel.ItemStatus = item.ItemStatus;
                itemViewModel.IsActive = item.IsActive;
                ICollection<Price> listPrice = item.Prices;
                var value = listPrice.Where(l => l.IsDelete != true).FirstOrDefault();
                if (value != null)
                {
                    itemViewModel.Prices = value.UnitPrice;
                }
                itemViewModels.Add(itemViewModel);
                cateIDRoot = item.CategoryId;//gan catId goc = catID trong lan loop nay 
                if (isNewCat)//chi add 1 lan category cua 1 nhom item
                {
                    itemFollowCategoryViewModel.categoryViewModel = new CategoryViewModel();
                    itemFollowCategoryViewModel.categoryViewModel.Id = item.CategoryId;
                    itemFollowCategoryViewModel.categoryViewModel.Name = item.CategoryName;
                    itemFollowCategoryViewModel.categoryViewModel.Thumbnail= item.Thumbnail;
                    isNewCat = false;//sau khi add xong thi set.false de ko add nua
                }

                if (sumCat == totalCat)
                {
                    itemFollowCategoryViewModel.listItemViewModel = itemViewModels;
                    myItemListView.MyItemListView.Add(itemFollowCategoryViewModel);
                }

            }
            return View(myItemListView);
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            //Lấy giá trong DB theo: Item Id, không bị delete, sắp xếp mới nhất, chọn đầu tiên.
            List<Price> listPrice = db.Prices.Where(p => p.ItemId == id & p.IsDelete != true).ToList();
            if (listPrice.Count > 0)
            {
                ViewBag.unitPrice = listPrice.OrderByDescending(p => p.PriceId).FirstOrDefault().UnitPrice;
            }
            ViewBag.categoryName = db.Categories.Where(c => c.CategoryId == item.CategoryId).FirstOrDefault().CategoryName;
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewData["listCategory"] = db.Categories.Where(c => c.IsDelete != true).OrderBy(c=>c.CategoryName).ToList();
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,ItemName,ItemInfo,IsActive,ItemStatus")] Item item, Price price)
        {
            HttpPostedFileBase thumb = Request.Files["imageUpload"];
            if (ModelState.IsValid)
            {
                if (thumb != null && thumb.ContentLength > 0)
                {
                    string pathToSave = Server.MapPath("~/Images/Uploads");
                    string fileName = thumb.FileName;
                    thumb.SaveAs(Path.Combine(pathToSave, fileName));
                    item.ThumbnailPath = "/Images/Uploads/" + fileName;
                }
                try
                {
                    //Add Item into Database
                    db.Items.Add(item);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return View(item);
                }

                //add price into database
                //Lấy Id của item mới tạo
                
                price.ItemId = db.Items.OrderByDescending(i => i.ItemId).FirstOrDefault().ItemId;
                price.DateStart = DateTime.Now;
                db.Prices.Add(price);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            ViewData["listCategory"] = db.Categories.Where(c => c.IsDelete != true).ToList();
            //Lấy giá trong DB theo: Item Id, không bị delete, sắp xếp mới nhất, chọn đầu tiên.
            Price listPrice = item.Prices.Where(p=>p.IsDelete!= true).FirstOrDefault();
            if (listPrice!= null)
            {
                ViewBag.unitPrice = listPrice.UnitPrice;
            }
            //Lay khuyen mai theo: itemId, khong bi delete
            //ViewBag.Amount = db.Promotions.Where(p => p.IsDelete != null & p.ItemId == item.ItemId).FirstOrDefault().Amount;

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CategoryId,ItemName,ItemInfo,IsActive,ItemStatus,IsDelete")] Item itemInput, Price price, string hf_ThumbnailPath)
        {
            HttpPostedFileBase thumb = Request.Files["imageUpload"];
            if (ModelState.IsValid)
            {
                //edit thumbnail
                //Nếu có chọn ảnh đại diện mới
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
                    itemInput.ThumbnailPath = "/Images/Uploads/" + fileName;
                }
                else
                {

                    itemInput.ThumbnailPath = hf_ThumbnailPath;
                }
                Item it = db.Items.Find(itemInput.ItemId);
                it.CategoryId = itemInput.CategoryId;
                it.ItemName = itemInput.ItemName;
                it.ItemInfo = itemInput.ItemInfo;
                it.IsActive = itemInput.IsActive;
                it.IsDelete = itemInput.IsDelete;
                it.ThumbnailPath = itemInput.ThumbnailPath;

                db.SaveChanges();
                //edit price
                //Lấy list Price theo: Item id, chưa bị delete
                Price pri = db.Prices.Where(p => p.ItemId == itemInput.ItemId & p.IsDelete != true).FirstOrDefault();
                if (pri != null) //neu co gia truoc do roi thi
                {
                    //Nếu giá cũ = giá mới => không thay đổi.
                    //Nếu giá cũ!= giá mới => xóa tất cả giá cũ, chèn giá mới.
                    if (pri.UnitPrice != price.UnitPrice)
                    {
                        pri.IsDelete = true;
                    }
                }
                price.DateStart = DateTime.Now;
                db.Prices.Add(price);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(itemInput);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            item.IsDelete = true;
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
