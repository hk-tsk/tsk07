using DACommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DA
{
    public interface IBaseRepository<T>
        where T : BaseEntity
    {
        T Insert(T entity);
        T Update(T entity);
        void Delete(T entity);
        int SaveChanges();

        IEnumerable<T> Query();
        IEnumerable<T> Query<K>(Expression<Func<T, K>> include);
        IQueryable<T> GetQuery();
    }
}
