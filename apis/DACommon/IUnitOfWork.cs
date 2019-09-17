using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DACommon
{
    public interface IUnitOfWork
    {
        T Insert<T>(T entity) where T : DACommon.Entities.BaseEntity, new();
        T Update<T>(T entity) where T : DACommon.Entities.BaseEntity, new();
        void Delete<T>(T entity) where T : DACommon.Entities.BaseEntity, new();
        int SaveChanges();

        IEnumerable<T> Query<T>() where T : DACommon.Entities.BaseEntity, new();
        IEnumerable<T> Query<T,K>(Expression<Func<T,K>> include) where T : DACommon.Entities.BaseEntity, new();
        IQueryable<T> GetAll<T>() where T : DACommon.Entities.BaseEntity, new();
    }
}
