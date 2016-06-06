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
    public class DashBoardService
    {
        private IRepository<Employees> db;

        public DashBoardService()
        {
            db = new GenericRepository<Employees>();
        }

        public List<EmpOrdersViewModel> GetEmpOrders()
        {
            List<EmpOrdersViewModel> models = new List<EmpOrdersViewModel>();

            var DbResult = db.Get().ToList(); 
            
            //將DAL取出的客戶資料跑迴圈丟進我們的ViewModel並回傳
            Mapper.CreateMap<Employees, EmpOrdersViewModel>()
                .ForMember(x => x.EmplyeeName, y => y.MapFrom(d => string.Format("{0} {1}", d.FirstName, d.LastName)))
                .ForMember(c => c.OrderCount, a => a.MapFrom(b => b.Orders.Count()));
            return Mapper.Map<List<Employees>, List<EmpOrdersViewModel>>(DbResult)
                .OrderByDescending(x => x.OrderCount).ToList();

        }
    }
}
