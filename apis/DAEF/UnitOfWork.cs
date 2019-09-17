using System.Collections.Generic;
using System.Linq;
using DACommon;
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
        public void Delete<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> GetAll<T>()
        {
            return dbContext.Courses.AsQueryable() as IQueryable<T>;
        }

        public T Insert<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> Query<T>()
        {
            throw new System.NotImplementedException();
        }

        public int SaveChanges<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public T Update<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

    }
}
