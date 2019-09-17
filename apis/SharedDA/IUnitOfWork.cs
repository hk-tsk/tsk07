using System.Collections.Generic;

namespace SharedDA
{
    public interface IUnitOfWork
    {
        T Insert<T>(T entity);
        T Update<T>(T entity);
        void Delete<T>(T entity);
        int SaveChanges<T>(T entity);

        IEnumerable<T> Query<T>();
    }
}
