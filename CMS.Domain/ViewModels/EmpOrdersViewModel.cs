using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.ViewModels
{
    class EmpOrdersViewModel
    {
        //員工編號PK
        public int EmplyeeID { get; set; }
        //員工姓名
        public string EmplyeeName { get; set; }
        //職稱
        public string Title { get; set; }
        //訂單數量
        public int OrderCount { get; set; }
    }
}
