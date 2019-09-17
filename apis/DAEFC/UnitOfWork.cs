using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DACommon;
using DACommon.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAEF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LearningSiteDBContext dbContext;
        public UnitOfWork(LearningSiteDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> getDbSet<T>()
            where T : DACommon.Entities.BaseEntity
        {
            return dbContext.Set<T>();
        }
        public void Delete<T>(T entity) where T : DACommon.Entities.BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> GetAll<T>() where T : DACommon.Entities.BaseEntity, new()
        {
            return getDbSet<T>().AsQueryable();
        }

        public T Insert<T>(T entity) where T : DACommon.Entities.BaseEntity, new()
        {
            getDbSet<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> Query<T>() where T : DACommon.Entities.BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public int SaveChanges() 
        {
            return dbContext.SaveChanges();
        }

        public T Update<T>(T entity) where T : DACommon.Entities.BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> Query<T, K>(Expression<Func<T, K>> include) where T : BaseEntity, new()
        {
            return getDbSet<T>().Include(include).AsQueryable();
        }
    }
}
