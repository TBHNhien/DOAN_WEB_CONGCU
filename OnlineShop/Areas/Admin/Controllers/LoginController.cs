using Models;
using OnlineShop.Areas.Admin.Code;
using OnlineShop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login

        [HttpGet]//thuộc tính có thể nhận Url
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]//không thể nhận thuộc tính Url
        [ValidateAntiForgeryToken]//trên server sinh ra 1 token => client 1 token tương tự => cả 2 khớp với nhau đồng bộ => chống việc request liên tục
        public ActionResult Index(LoginModel model)
        {
            // Thực hiện kiểm tra đăng nhập
            //var result = new AccountModel().Login(model.UserName, model.Password);
            //if (result && ModelState.IsValid)
            //{
            //    // C1 Kiểm tra có giữ session không, nếu có thì xoá đi để tạo session mới
            //    //var existingSession = SessionHelper.GetSession();


            //    if (existingSession != null)
            //    {
            //        Session.Abandon();
            //    }

            //    // Set session mới
            //    SessionHelper.SetSession(new UserSession() { UserName = model.UserName });

            //    // Chuyển hướng đến trang Index của controller Home
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    // Logic xử lý khi đăng nhập không thành công
            //    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            //}
            //    return View(model);

            //C2 DÙNG Membership
            if (Membership.ValidateUser(model.UserName,model.Password) && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Logic xử lý khi đăng nhập không thành công
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            return View(model);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();//huỷ tất cả cookies vs session đc tạo 
            return RedirectToAction("Index", "Login");
        }

    }
}