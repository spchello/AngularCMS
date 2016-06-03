using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CMS.DAL.Interfaces;
using CMS.Domain;
using System.Linq.Expressions;

namespace CMS.DAL.Repository
{
    //實作IRepository介面
    //各個function基本上就是Entity Freamwork的新增修改刪除功能
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal NorthwindEntities db;
        internal DbSet<TEntity> dbTable; //dal層要先去NuGet package安裝EntityFramework

        public GenericRepository()
        {
            db = new NorthwindEntities(); //db
            dbTable = db.Set<TEntity>();  //db中的某個tableC:\VSNETprojects\angularCMS\CMS.DAL\packages.config
        }

        //刪除某筆資料 by Entity
        public void Delete(TEntity entity)
        {
            //假如Entity處於Detached狀態，就先Attach起來，這樣才能順利移除
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbTable.Attach(entity);
            }

            dbTable.Remove(entity);
            db.SaveChanges();
        }


        //刪除某筆資料 by ID
        public void Delete(object id)
        {
            TEntity entityToDelete = dbTable.Find(id);
            Delete(entityToDelete);
            db.SaveChanges();
        }


        //實作Get，通常我們查詢都會帶有一些條件，故可以用以下方式動態的的帶入filter、orderby參數，而不用先取得所有資料回傳後再去做篩選與排序。
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {

            //取得所有資料
            IQueryable<TEntity> queryData = dbTable;

            if (filter != null)
            {
                queryData = queryData.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(queryData);
            }
            else
            {
                return queryData;
            }
        }


        //取得某筆資料
        public TEntity GetByID(object id)
        {
            return dbTable.Find(id);
        }

        //新增一筆資料
        public void Insert(TEntity entity)
        {
            dbTable.Add(entity);
            db.SaveChanges();
        }

        //修改資料
        public void Update(TEntity entity)
        {
            dbTable.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}

