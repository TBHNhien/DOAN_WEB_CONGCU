using OnlineShop_10.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop_10.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        //public ActionResult Index()
        //{
        //    return View();
        //}

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session==null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login" , action = "Index" , Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}