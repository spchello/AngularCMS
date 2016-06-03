using CMS.DAL.Interfaces;
using CMS.DAL.Repository;
using CMS.Domain;
using CMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BLL.Services
{
    // 呼叫DAL取得DB資料，並轉換成我們需要的ViewModel
    public class CustomerService
    {
        //指定這個Repositoy使用Customers資料表
        private IRepository<Customers> db;

        public CustomerService()
        {
            db = new GenericRepository<Customers>();
        }

        //取得所有客戶資料
        public IQueryable<CustomerViewModel> Get()
        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();
            var DbResult = db.Get().AsQueryable(); //將DAL取出的客戶資料跑迴圈丟進我們的ViewModel並回傳
            foreach (var item in DbResult)
            {
                CustomerViewModel _model = new CustomerViewModel();
                _model.CustomerID = item.CustomerID;
                _model.ContactName = item.ContactName;
                _model.CompanyName = item.CompanyName;
                model.Add(_model);
            }
            return model.AsQueryable();
        }
     
    }
}
