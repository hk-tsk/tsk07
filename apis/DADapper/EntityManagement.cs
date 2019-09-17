using DACommon;
using DACommon.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DADapper
{
    public class EntityManagement
    {

        private readonly static object lockObject = new object();
        private readonly Dictionary<string, EntityMetaData> AllMetaData;
        private static EntityManagement instance;

        private EntityManagement()
        {
            AllMetaData = new Dictionary<string, EntityMetaData>();
            this.Init();
        }

        public static EntityManagement GetInstance()
        {
            if (EntityManagement.instance == null)
            {
                lock (lockObject)
                {
                    if (EntityManagement.instance == null)
                        EntityManagement.instance = new EntityManagement();
                }
            }

            return EntityManagement.instance;
        }

        public EntityMetaData GetMetaData(Type entity)
        {
            return this.AllMetaData.FirstOrDefault(md => md.Key == GetName(entity)).Value;
        }
        private void Init()
        {

            var allEntities = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract && t.Namespace.IndexOf("SharedDA.Entities") > -1 &&
                typeof(BaseEntity).IsAssignableFrom(t));

            foreach (var item in allEntities)
            {
                AllMetaData.Add(item.Name, FillEntityMetaData(item));
            }

        }

        private EntityMetaData FillEntityMetaData(Type item)
        {
            var md = new EntityMetaData();
            md.TableName = GetName(item);
            md.ColumnMetaData = GetColumnMetaData(item);
            return md;
        }

        private ColumnMetaData[] GetColumnMetaData(Type item)
        {
            var cols = new List<ColumnMetaData>();

            foreach (var info in item.GetProperties())
            {
                if (!info.PropertyType.Name.ToLower().Contains("DACommon.Entities"))
                {
                    var col = new ColumnMetaData();
                    col.Name = info.Name;
                    col.Param = "@" + info.Name;

                    switch (info.PropertyType.Name.ToLower())
                    {
                        case "short":
                            col.DBType = DbType.Int16;
                            break;
                        case "int":
                            col.DBType = DbType.Int32;
                            break;
                        case "long":
                            col.DBType = DbType.Int64;
                            break;
                        case "double":
                            col.DBType = DbType.Double;
                            break;
                        case "boolean":
                            col.DBType = DbType.Boolean;
                            break;
                        default:
                            col.DBType = DbType.String;
                            break;
                    }
                    cols.Add(col);

                }
            }

            return cols.ToArray();
        }

        private static string GetName(Type item)
        {
            return item.GetCustomAttribute<TableName>().Name;
        }


    }

    public class EntityMetaData
    {
        public string TableName { get; set; }
        public ColumnMetaData[] ColumnMetaData { get; set; }
        public string[] Columns
        {
            get
            {
                return this.ColumnMetaData.Select(p => p.Name).ToArray();


            }

        }
        public string[] Params
        {
            get
            {
                return this.ColumnMetaData.Select(p => p.Param).ToArray();
            }

        }

    }

    public class ColumnMetaData
    {
        public string Name { get; set; }
        public string Param { get; set; }
        public DbType DBType { get; set; }
    }

}