using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using lengkeng.Models;
namespace lengkeng.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoLogin(UserDetails userDetail)
        {
            if (ModelState.IsValid)
            {
                AccountBusinessLayer accountBusinessLayer = new AccountBusinessLayer();
                UserStatus userstatus = accountBusinessLayer.validUser(userDetail);
                if (userstatus == UserStatus.Khach)
                {
                    ModelState.AddModelError("LoginError", "Usename hoặc Password không đúng.");
                    return View("Login");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userDetail.Username, false);
                    if (userstatus == UserStatus.Admin)
                    {
                        HttpContext.Session["IsAdmin"] = true;
                    }else if (userstatus == UserStatus.Quanly)
                    {
                        HttpContext.Session["IsQuanly"] = true;
                    }
                    else if (userstatus == UserStatus.Thungan)
                    {
                        HttpContext.Session["IsThungan"] = true;
                    }
                    else if (userstatus == UserStatus.Boiban)
                    {
                        HttpContext.Session["IsBoiban"] = true;
                    }
                    
                    return RedirectToAction("Index", "News");
                }
            }
            ModelState.AddModelError("LoginError", "Usename hoặc Password không hợp lệ.");
            return RedirectToAction("Index", "News");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}