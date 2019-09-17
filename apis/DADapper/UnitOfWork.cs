using DACommon;
using DACommon.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace DADapper
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly DbConnection Connection;
        private readonly EntityManagement entityManagement;

        public UnitOfWork(DbConnection connection)
        {
            this.Connection = connection;
            entityManagement = EntityManagement.GetInstance();
        }


        private EntityMetaData getMetaData<T>()
        {
            return entityManagement.GetMetaData(typeof(T));
        }

        private string TableName<T>()
        {
            return "dbo." + getMetaData<T>().TableName;
        }

        private string Columns<T>()
        {
            var cols = string.Join(",", getMetaData<T>().Columns);
            return cols;
        }

        private string ColumnsParams<T>()
        {
            var cols = string.Join(",", getMetaData<T>().Params);
            return cols;
        }

        private DynamicParameters InsertParams<T>(T entity)
        {
            DynamicParameters insertParams = new DynamicParameters();
            foreach (var item in getMetaData<T>().ColumnMetaData)
            {
                var value = entity.GetType().GetProperty(item.Name).GetValue(entity);
                insertParams.Add(item.Param, value, item.DBType);
            }
            return insertParams;
        }

        public IEnumerable<T> Query<T>() where T : DACommon.Entities.BaseEntity, new()
        {
            string sql = "select " + Columns<T>() + " from " + TableName<T>();
            return Connection.Query<T>(sql);
        }

        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public T Insert<T>(T entity) where T : DACommon.Entities.BaseEntity, new()
        {
            string sql = "insert into " + TableName<T>() + "(" + Columns<T>() + ") " + " values(" + string.Join(", ", ColumnsParams<T>()) + ")";
            Connection.Execute(sql, InsertParams(entity));
            return entity;
        }

        public T Update<T>(T entity) where T : DACommon.Entities.BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : DACommon.Entities.BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity, new()
        {
            // Connection.Query()
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> Query<T, K>(Expression<Func<T, K>> include) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }
    }
}
