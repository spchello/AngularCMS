using CMS.BLL.Services;
using CMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        //Submit接收ViewModel
        [HttpPost, ActionName("UserLogin")]
        public ActionResult LoginForCollege(LoginViewModel model)
        {
            var UserID = LoginService.CheckUser(model);
            SetLogin(UserID, "Admin");
            return RedirectToAction("Index", "Order");
        }

        private void SetLogin(int UserID, string Role)
        {
            var now = DateTime.Now;
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: UserID.ToString(),
                issueDate: now,
                expiration: now.AddHours(12), //設定登入過期時間
                isPersistent: true,
                userData: Role + ",", //寫入權限管理的角色
                cookiePath: FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            //清除所有的 Session
            Session.RemoveAll();

            //
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "Login");
        }
    }
}