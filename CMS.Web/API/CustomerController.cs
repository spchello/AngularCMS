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

        //// GET: api/Customer
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Customer/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Customer
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Customer/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Customer/5
        //public void Delete(int id)
        //{
        //}
    }
}
