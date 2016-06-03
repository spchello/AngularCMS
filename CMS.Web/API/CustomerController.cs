using CMS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS.Web.API
{
    public class CustomerController : ApiController
    {

        //向BLL層要客戶資料
        //private CMS.BLL.Services.CustomerService service;
        private CustomerService service;

        //建構子
        public CustomerController()
        {
            service = new CustomerService(); //BLL層
        }

        [HttpGet]
        //向BLL層要客戶資料
        public HttpResponseMessage Customers()
        {
            try
            {
                var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        [HttpGet]
        //向BLL層要分頁的資料
        public HttpResponseMessage Customers(int CurrPage, int PageSize)
        {
            try
            {
                //總數量
                int TotalRaw = 0;
                //向BLL取得資料
                var datas = service.Get(CurrPage, PageSize, out TotalRaw);
                //回傳一個JSON Object
                var Rvl = new { Total = TotalRaw, Data = datas };
                return Request.CreateResponse(HttpStatusCode.OK, Rvl);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

    }
}
