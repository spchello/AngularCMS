using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DAL.Interfaces
{
    //Repository Pattern（倉儲模式），將新增、修改、查詢、刪除等動作封裝到一個Class，有關這個Entity的這些動作，都封裝在此，不會在其他專案裡存在著相同的程式碼
    //介面 要做的事是要專注在新增，修改，刪除
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        TEntity GetByID(object id);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
    }
}
