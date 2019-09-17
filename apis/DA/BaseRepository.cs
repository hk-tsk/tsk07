using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DACommon;
using DACommon.Entities;

namespace DA
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity, new()
    {

        private readonly IUnitOfWork unitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Delete(T entity)
        {
            unitOfWork.Delete<T>(entity);
        }

        public IQueryable<T> GetQuery()
        {
            return unitOfWork.GetAll<T>();
        }

        public T Insert(T entity)
        {
            return unitOfWork.Insert<T>(entity);
        }

        public IEnumerable<T> Query()
        {
            return unitOfWork.Query<T>();
        }

        public IEnumerable<T> Query<K>(Expression<Func<T, K>> include)
        {
            return unitOfWork.Query<T, K>(include);
        }

        public int SaveChanges()
        {
            return unitOfWork.SaveChanges();
        }

        public T Update(T entity)
        {
            return unitOfWork.Update<T>(entity);
        }
    }
}