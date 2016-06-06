using AutoMapper;
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
            //List<CustomerViewModel> model = new List<CustomerViewModel>();
            //var DbResult = db.Get().AsQueryable(); //將DAL取出的客戶資料跑迴圈丟進我們的ViewModel並回傳
            //foreach (var item in DbResult)
            //{
            //    CustomerViewModel _model = new CustomerViewModel();
            //    _model.CustomerID = item.CustomerID;
            //    _model.ContactName = item.ContactName;
            //    _model.CompanyName = item.CompanyName;
            //    model.Add(_model);
            //}
            //return model.AsQueryable();

            var DbResult = db.Get().AsQueryable(); //將DAL取出的客戶資料跑迴圈丟進我們的ViewModel並回傳
            Mapper.CreateMap<Customers, CustomerViewModel>();
            return Mapper.Map<IQueryable<Customers>, IQueryable<CustomerViewModel>>(DbResult);
        }


        //取得所有客戶資料(分頁)
        public IQueryable<CustomerViewModel> Get(int CurrPage, int PageSize, out int TotalRaw)
        {
            //取得所有筆數
            TotalRaw = db.Get().ToList().Count();
            //使用Linq篩選分頁
            var DbResult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            //Mapping到ViewModel
            Mapper.CreateMap<Customers, CustomerViewModel>();
            return Mapper.Map<List<Customers>, List<CustomerViewModel>>(DbResult).AsQueryable();
        }


        public void AddCustomer(CustomerViewModel models)
        {
            Mapper.CreateMap<CustomerViewModel, Customers>();
            var cust = Mapper.Map<CustomerViewModel, Customers>(models);
            db.Insert(cust);
        }


        public void SaveCustomer(CustomerViewModel models)
        {
            Mapper.CreateMap<CustomerViewModel, Customers>();
            var cust = Mapper.Map<CustomerViewModel, Customers>(models);
            db.Update(cust);
        }

        public void Delete(string CustomerID)
        {
            var Customer = db.GetByID(CustomerID);
            db.Delete(Customer);
        }

    }
}
