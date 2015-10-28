using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lengkeng.DataAccessLayer;
using lengkeng.Models;
using lengkeng.ViewModels;

namespace lengkeng.Filters
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult v = filterContext.Result as ViewResult;
            if (v != null) // v will null when v is not a ViewResult
            {
                BaseViewModel bvm = v.Model as BaseViewModel;
                if (bvm != null)//bvm will be null when we want a view without Header and footer
                {
                    //bvm.UserName = HttpContext.Current.User.Identity.Name;

                    //header
                    bvm.HeaderViewModel = new HeaderViewModel();
                    DBContext db = new DBContext();
                    var queryItem = from cat in db.Categories
                                    join item in db.Items on cat.CategoryId equals item.CategoryId
                                    where item.IsDelete != true
                                    group cat by item.CategoryId into g
                                    select new { g.Key, g.FirstOrDefault().CategoryName };
                    List<Category> listCategory = db.Categories.ToList();

                    //List<Category> listCategory = new DBContext().Categories.Where(c => c.IsDelete != true).ToList();
                    List<CategoryViewModel> listCategoryViewModel = new List<CategoryViewModel>();
                    foreach (var item in queryItem)
                    {
                        CategoryViewModel category = new CategoryViewModel();
                        category.Id = item.Key;
                        category.Name = item.CategoryName;
                        listCategoryViewModel.Add(category);
                    }
                    bvm.HeaderViewModel.listCategoryViewModel = listCategoryViewModel;
                    bvm.HeaderViewModel.Logo = "/Images/imgs/logo.png";

                    //list slide from Feature table
                    List<SliderViewModel> listSliderViewModel = new List<SliderViewModel>();
                    List<Feature> listfeature = db.Features.Where(f => f.IsDelete != true).ToList();
                    SliderViewModel slideViewModel;
                    foreach (var fea in listfeature)
                    {
                        slideViewModel = new SliderViewModel();
                       
                        if (fea.IsOnlyImage)
                        {
                            slideViewModel.Background = fea.BackgroundImage;
                            slideViewModel.IsOnlyImage = true;
                        }
                        else
                        {
                            slideViewModel.IDDiv = fea.Id;
                            slideViewModel.Title = fea.Title;
                            slideViewModel.Info = fea.Description;
                            slideViewModel.Background = fea.BackgroundImage;
                            slideViewModel.IsOnlyImage = false;
                            slideViewModel.Thumb1 = fea.ThumbnailPath1;
                            slideViewModel.Thumb2 = fea.ThumbnailPath2;
                        }
                        listSliderViewModel.Add(slideViewModel);
                    }
                    bvm.HeaderViewModel.listSliderViewModel = listSliderViewModel;
                    //footer
                    bvm.FooterViewModel = new FooterViewModel();
                    Contact cont = new DBContext().Contacts.Where(c => c.IsDelete != true).OrderByDescending(c => c.Id).FirstOrDefault();
                    bvm.FooterViewModel.Email = cont.Email;//Can be set to dynamic value
                    bvm.FooterViewModel.Tel = cont.Tel;
                    bvm.FooterViewModel.Address = cont.Address;
                    bvm.FooterViewModel.CopyRightBy = "LengKeng";
                    bvm.FooterViewModel.CopyRightYear = "2015";
                    bvm.FooterViewModel.CopyRightLink = "http://lengkeng.net.vn";
                }
            }
        }
    }
}