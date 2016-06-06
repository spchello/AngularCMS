using CMS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS.Web.API
{
    public class DashBoardController : ApiController
    {
        private DashBoardService service;

        public DashBoardController()
        {
            service = new DashBoardService(); //BLL層
        }


        //向BLL層要資料
        public HttpResponseMessage Get()
        {
            try
            {
                var datas = service.GetEmpOrders();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
    }
}
