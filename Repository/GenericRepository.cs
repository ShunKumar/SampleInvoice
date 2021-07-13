using ApplicationModel.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity :class
    {
        #region Property and Constructor region
        private readonly AppDbContext dbContext;
        private readonly DbSet<TEntity> dbset;
        public GenericRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbset = _dbContext.Set<TEntity>();
        }
        #endregion

        #region Get call regions
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeproperties = "")
        {
            IQueryable<TEntity> query = dbset;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (var includeProperty in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbset;
        }

        public TEntity GetByID(object id)
        {
            return dbset.Find(id);
        }
        #endregion

        #region first single call related region
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return dbset.First(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return dbset.Single(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return dbset.SingleOrDefault(predicate);
        }
        #endregion

        #region update table region
        public void Insert(TEntity entity)
        {
            dbset.Add(entity);
        }
        public void Update(TEntity entity)
        {
            dbset.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        #endregion

        #region Delete Call region
        public void Delete(object id)
        {
            TEntity entityToDelete = dbset.Find(id);
            Delete(entityToDelete);
        }
        public void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
        }

        public void DeleteList(List<int> id)
        {
            foreach (var i in id)
            {
                TEntity entityToDelete = dbset.Find(i);
                Delete(entityToDelete);
            }
        }
        #endregion
    }
}
