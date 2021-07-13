using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeproperties = "");
        TEntity GetByID(object id);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity First(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);
        void Delete(object id);
        void DeleteList(List<int> id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
