using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lengkeng.Models;

namespace lengkeng.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"]))
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "Bạn không có quyền truy cập vào đây. Vui lòng <a href='/Home/Index'>Trở về trang chủ</a"
                };
            }
        }
    }
}