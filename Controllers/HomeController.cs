using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lengkeng.DataAccessLayer;
using lengkeng.ViewModels;
using lengkeng.Models;
using lengkeng.Filters;
namespace lengkeng.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private  DBContext db = new DBContext();
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            Welcome welcome = db.Welcomes.Where(w => w.IsDelete != true).OrderByDescending(w => w.Id).FirstOrDefault();
            HomeWelcomeViewModel homeWelcome = new HomeWelcomeViewModel();
            if (welcome != null)
            {
                homeIndexViewModel.homeWelcomeVM = new HomeWelcomeViewModel();

                homeWelcome.Title = welcome.Title;
                homeWelcome.Content = welcome.Content;
                homeWelcome.Background = welcome.Background;
                homeWelcome.VideoYoutubeId = welcome.LinkVideo;
                
                homeIndexViewModel.homeWelcomeVM = homeWelcome;// finished Home Welcom
            }
            

            //homeIndexViewModel.listServicVM
            List<HomeServiceViewModel> listService = new List<HomeServiceViewModel>();
            var query = db.Services.Where(s => s.IsDelete != true).OrderByDescending(s => s.Id).Take(6).ToList();
            HomeServiceViewModel servicModel;
            for(int i = 0; i<query.Count() ;i++)
            {
                servicModel = new HomeServiceViewModel();
                servicModel.Id = query[i].Id;
                servicModel.Name = query[i].Name;
                servicModel.Info = query[i].Info;
                servicModel.Delay = i * 25;
                listService.Add(servicModel);
            }
            homeIndexViewModel.listServicVM = new List<HomeServiceViewModel>();
            homeIndexViewModel.listServicVM = listService;//finish home service

            List<HomeNewsViewModel> listNews = new List<HomeNewsViewModel>();
            var queryNews = db.Newss.Where(n => n.IsDelete != true).OrderByDescending(n => n.DatePost).Take(8).ToList();
            HomeNewsViewModel NewsModel;
            foreach (var news in queryNews)
            {
                NewsModel = new HomeNewsViewModel();
                NewsModel.Id = news.Id;
                NewsModel.Title = news.Title;
                NewsModel.Content = news.Content;
                NewsModel.Thumb = news.Thumb;
                listNews.Add(NewsModel);
            }
            homeIndexViewModel.listNewsVM = new List<HomeNewsViewModel>();
            homeIndexViewModel.listNewsVM = listNews;//finish home news

            YoutubeHome youtubehome = db.YoutubeHomes.Where(y => y.IsDelete != true).OrderByDescending(y => y.Id).FirstOrDefault();
            HomeVideoViewModel homeVideo = new HomeVideoViewModel();
            if (youtubehome != null)
            {
                homeIndexViewModel.homeVideoVM = new HomeVideoViewModel();
                homeVideo.Title = youtubehome.Title;
                homeVideo.Content = youtubehome.Content;
                homeVideo.Thumb = youtubehome.Thumb;
                homeVideo.YoutubeId1 = youtubehome.YoutubeId1;
                homeVideo.YoutubeId2 = youtubehome.YoutubeId2;
                homeIndexViewModel.homeVideoVM = homeVideo;//finish home video
            }
            SteptoCreate steptoCreate = db.SteptoCreates.Where(s => s.IsDelete != true).OrderByDescending(s => s.Id).FirstOrDefault();
            HomeStepViewModel HomeStep = new HomeStepViewModel();
            if (steptoCreate != null)
            {
                homeIndexViewModel.homeStepVM = new HomeStepViewModel();
                HomeStep.Title = steptoCreate.Title;
                HomeStep.Steps = steptoCreate.Steps;
                HomeStep.Thumb = steptoCreate.ThumbnailPath;
                HomeStep.Recomand = steptoCreate.Slogan;
                homeIndexViewModel.homeStepVM = HomeStep;//finish home step
            }
            return View(homeIndexViewModel);
        }
        [HeaderFooterFilter]
        [HandleError]
        public ActionResult About()
        {
            AboutListViewModel aboutListViewModel = new AboutListViewModel();

            //add AboutViewModel
            List<AboutViewModel> listAVM = new List<AboutViewModel>();
            List<About> listAbout = new DBContext().Abouts.Where(a => a.IsDelete != true).OrderBy(a => a.Id).ToList();
            AboutViewModel AboutViewModel;
            foreach (var item in listAbout)
            {
                AboutViewModel = new AboutViewModel();
                AboutViewModel.Title = item.Title;
                AboutViewModel.Content = item.content;
                AboutViewModel.Thumbnail = item.Thumbnails;
                listAVM.Add(AboutViewModel);
            }
            aboutListViewModel.listAboutViewModel = listAVM;

            //Add Welcome
            Welcome welcome = new DBContext().Welcomes.Where(w => w.IsDelete != null).FirstOrDefault();
            if (welcome != null)
            {
                aboutListViewModel.WelcomeTitle = welcome.Title;
                aboutListViewModel.WelcomeContext = welcome.Content;
                aboutListViewModel.WelcomeBackground = welcome.Background;
                aboutListViewModel.WelcomeLinkVideo = welcome.LinkVideo;
            }
            return View(aboutListViewModel);
        }

        [HeaderFooterFilter]
        public ActionResult Contact()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            Contact contact = new DBContext().Contacts.FirstOrDefault();
            contactViewModel.Email = contact.Email;
            contactViewModel.Tel = contact.Tel;
            contactViewModel.Address = contact.Address;
            contactViewModel.DoContactAuthor = "";
            contactViewModel.DoContactEmail = "";
            contactViewModel.DoContactSubject = "";
            contactViewModel.DoContactMessage = "";

            if (HttpContext.Session["DoContact"] != null)
            {
                DoContact docontact = (DoContact)HttpContext.Session["DoContact"];
                contactViewModel.DoContactAuthor = docontact.Author;
                contactViewModel.DoContactEmail = docontact.Email;
                contactViewModel.DoContactSubject = docontact.Subject;
                contactViewModel.DoContactMessage = docontact.Message;
            }

            return View(contactViewModel);
        }

        public ActionResult DoContact(DoContact doContact)
        {
            if (ModelState.IsValid)
            {
                new DBContext().DoContacts.Add(doContact);
                new DBContext().SaveChanges();
            }
            else
            {
                //ContactViewModel contactViewModel = new ContactViewModel();
                //contactViewModel.DoContactAuthor = doContact.Author;
                //contactViewModel.DoContactEmail = doContact.Email;
                //contactViewModel.DoContactSubject = doContact.Subject;
                //contactViewModel.DoContactMessage = doContact.Message;
                HttpContext.Session["DoContact"] = doContact;
                return RedirectToAction("Contact");
            }
            return RedirectToAction("Index");
        }

        [HeaderFooterFilter]
        public ActionResult Menu()
        {
            DBContext db = new DBContext();
            MenuViewModel menuViewModel = new MenuViewModel();
            List<ItemFollowCategoryViewModel> itemFollowCategoryViewModels = new List<ItemFollowCategoryViewModel>();
            ItemFollowCategoryViewModel itemFollowCategoryViewModel = new ItemFollowCategoryViewModel();
            List<ItemViewModel> itemViewModels = new List<ItemViewModel>();

            var query = from cat in db.Categories
                        join item in db.Items.Where(i => i.IsDelete != true).OrderBy(i => i.CategoryId) on cat.CategoryId equals item.CategoryId
                        select new { cat.CategoryId, cat.CategoryName, cat.Thumbnail, item.ItemId, item.ItemName, item.ItemInfo, item.ItemStatus, item.Prices, item.Promotions, item.ThumbnailPath };
            int sumQUery = query.ToList().Count();
            int cateIDRoot = 0;
            bool isNewCat = false;
            int count = 0;
            foreach (var item in query)
            {
                count++;
                if (cateIDRoot == 0)//moi dau
                {
                    isNewCat = true;
                }
                else if (cateIDRoot != item.CategoryId) //truoc khi bat dau 1 category moi thi add cu, reset lai tu dau
                {
                    //neu catID lan loop nay khac voi goc va != 0, => la 1 nhom item moi
                    //add ItemViewModels vao itemFollowCategoryViewModel, itemFollowCategoryViewModel gio co du gia tri can roi, tiep tuc add no vao (list) itemFollowCategoryViewModels
                    //reset lai itemViewModels = null;
                    //reset lai itemFollowCategoryViewModel
                    //gan 1 flag = true de itemFollowCategoryViewModel them info cua cat vao
                    itemFollowCategoryViewModel.listItemViewModel = itemViewModels;
                    itemFollowCategoryViewModels.Add(itemFollowCategoryViewModel);
                    itemViewModels = new List<ItemViewModel>();
                    itemFollowCategoryViewModel = new ItemFollowCategoryViewModel();
                    isNewCat = true;
                }

                ItemViewModel itemViewModel = new ItemViewModel();
                itemViewModel.ItemId = item.ItemId;
                itemViewModel.ItemName = item.ItemName;
                itemViewModel.ThumbnailPath = item.ThumbnailPath;
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
                    itemFollowCategoryViewModel.categoryViewModel.Thumbnail = item.Thumbnail;
                    isNewCat = false;//sau khi add xong thi set.false de ko add nua
                }
                if (count == sumQUery) //khi day da la vong cuoi cung
                {
                    itemFollowCategoryViewModel.listItemViewModel = itemViewModels;
                    itemFollowCategoryViewModels.Add(itemFollowCategoryViewModel);
                }

            }
            menuViewModel.listItemFollowCategoryViewModel = itemFollowCategoryViewModels;
            return View(menuViewModel);
        }
        [HeaderFooterFilter]
        [HttpGet]
        public ActionResult DoMenu(int? id)
        {
            DBContext db = new DBContext();
            DoMenuViewModel doMenu = new DoMenuViewModel();
            doMenu.itemFollowCategoryViewModels = new ItemFollowCategoryViewModel();
            doMenu.itemFollowCategoryViewModels.categoryViewModel = new CategoryViewModel();
            var queryCat = from cat in db.Categories
                           join item in db.Items on cat.CategoryId equals item.CategoryId
                           where item.IsDelete != true
                           group cat by item.CategoryId into g
                           select new { g.Key, g.FirstOrDefault().CategoryName, g.FirstOrDefault().Thumbnail };
            List<Category> listCategory = db.Categories.ToList();
            List<CategoryViewModel> listCategoryViewModel = new List<CategoryViewModel>();
            CategoryViewModel category;
            foreach (var item in queryCat)
            {
                category = new CategoryViewModel();
                category.Id = item.Key;
                category.Name = item.CategoryName;
                category.Thumbnail = item.Thumbnail;
                listCategoryViewModel.Add(category);
                if (category.Id == id)
                {
                    doMenu.itemFollowCategoryViewModels.categoryViewModel = category;
                }
            }
            List<ItemViewModel> itemViewModels = new List<ItemViewModel>();
            ItemViewModel itemViewModel;
            var queryItem = from item in db.Items
                            where item.IsDelete != true && item.CategoryId == id
                            select item;
            foreach (var item in queryItem)
            {
                itemViewModel = new ItemViewModel();
                itemViewModel.ItemId = item.ItemId;
                itemViewModel.ItemName = item.ItemName;
                itemViewModel.ItemInfo = item.ItemInfo;
                if (item.Prices.ToList().Count != 0)
                {
                    ICollection<Price> listPrice = item.Prices;
                    var value = listPrice.Where(p => p.IsDelete != true).FirstOrDefault();

                    itemViewModel.Prices = value.UnitPrice;
                }
                itemViewModel.ThumbnailPath = item.ThumbnailPath;
                itemViewModels.Add(itemViewModel);
            }

            //cho menu o tren
            doMenu.categoryViewModels = listCategoryViewModel;

            //cho danh sach cac item cua menu voi id can tim
            doMenu.itemFollowCategoryViewModels.listItemViewModel = itemViewModels;


            return View(doMenu);
        }

        [HeaderFooterFilter]
        public ActionResult News()
        {
            return View();
        }

        [HeaderFooterFilter]
        public ActionResult Galary()
        {
            return View();
        }
    }
}