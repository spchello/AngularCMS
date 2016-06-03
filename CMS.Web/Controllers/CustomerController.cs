using CMS.Domain.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            ////使用WebClinet 從Web API取得資料
            //WebClient client = new WebClient();
            //client.Headers["Accept"] = "application/json";
            //string rvl = client.DownloadString(new Uri("http://localhost:11733/api/Customer"));
            ////將Json字串轉成ViewModel
            //IList<CustomerViewModel> models = JsonConvert.DeserializeObject<IList<CustomerViewModel>>(rvl);
            ////將ViewModel傳給View使用
            //return View(models);

            return View();
        }
    }
}