using DACommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DAEFC.EntityConfigurations
{
    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T : DACommon.Entities.BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigurationEngine.config<T>(builder);
        }
    }

    public class ConfigurationEngine
    {

        public static void config<T>(EntityTypeBuilder<T> builder) where T : DACommon.Entities.BaseEntity
        {
            Type item = typeof(T);

            builder.ToTable(item.GetCustomAttribute<TableName>().Name);

            var properties = item.GetProperties();

            List<PropertyInfo> refList = properties
                .Where(p => p.GetCustomAttributes().Any(c => c.GetType() == typeof(ReferenceEntity))).ToList();

            List<string> refColNameList = refList.Select(p => p.GetCustomAttribute<ReferenceEntity>().Name).ToList();

            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    builder.Property(prop.PropertyType, prop.Name)
                        .IsRequired();

                    builder.HasKey(prop.Name);
                }
                else
                {
                    var attrs = prop.GetCustomAttributes();
                                       
                    if (attrs.Any(a => a.GetType() == typeof(ReferenceEntity)))
                    {
                        ReferenceEntity refAtt = attrs.First(a => a.GetType() == typeof(ReferenceEntity)) as ReferenceEntity;

                        Type colType = prop.PropertyType;
                        //builder.HasIndex(refAtt.Name);
                        string refCollectionName = prop.PropertyType.GetProperties()
                            .Where(p => p.PropertyType.IsGenericType &&
                                    p.PropertyType.GenericTypeArguments.Any(a => a.GetType() == colType.GetType())).First().Name;

                        var CreateLambdaExpressionMethod = typeof(ConfigurationEngine).GetMethod("CreateLambdaExpression", BindingFlags.Static | BindingFlags.Public);
                        CreateLambdaExpressionMethod = CreateLambdaExpressionMethod.MakeGenericMethod(item, prop.PropertyType);

                        var methodParam = new ArrayList(3);
                        methodParam.Add(builder);
                        methodParam.Add(prop);
                        methodParam.Add(refCollectionName);
                        methodParam.Add(refAtt.Name);
                        CreateLambdaExpressionMethod.Invoke(null, methodParam.ToArray());
                    }
                    else if (!refColNameList.Any(c => c.Trim() == prop.Name.Trim()) && !prop.PropertyType.IsGenericType)
                    {
                        bool hasRequired = attrs.Any(c => c.GetType() == typeof(Required)) || prop.PropertyType == typeof(bool);
                        bool isunique = attrs.Any(c => c.GetType() == typeof(IsUnique));
                        bool longText = attrs.Any(c => c.GetType() == typeof(LongText));
                        MaxLength maxLength = attrs.FirstOrDefault(c => c.GetType() == typeof(MaxLength)) as MaxLength;
                        bool isConcurrncyToken = attrs.Any(c => c.GetType() == typeof(ConcurrencyToken));

                        var propBuilder = builder.Property(prop.PropertyType, prop.Name)
                             .IsRequired(hasRequired)
                             .IsConcurrencyToken(isConcurrncyToken);

                        if (prop.PropertyType == typeof(bool))
                            propBuilder.HasDefaultValue(false);

                        if (prop.PropertyType == typeof(string))
                            propBuilder.HasMaxLength(longText ? 5000 : (maxLength != null ? maxLength.Count : 300));

                        if (isunique)
                        {
                            builder.HasIndex(prop.Name)
                                 .IsUnique();
                        }
                    }
                }
            }
        }

        public static void CreateLambdaExpression<T, K>(EntityTypeBuilder<T> builder,
                    PropertyInfo prop, string refCollectionName, string foreignName)

            where T : DACommon.Entities.BaseEntity
            where K : DACommon.Entities.BaseEntity
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression right = Expression.Property(param, typeof(T).GetProperty(prop.Name));
            Expression<Func<T, K>> exp = Expression.Lambda<Func<T, K>>(right, param);

            ParameterExpression paramM = Expression.Parameter(typeof(K), "x");
            Expression rightM = Expression.Property(paramM, typeof(K).GetProperty(refCollectionName));
            Expression<Func<K, IEnumerable<T>>> manyExp = Expression.Lambda<Func<K, IEnumerable<T>>>(rightM, paramM);

            builder.HasOne(exp)
                        .WithMany(manyExp)
                        .HasForeignKey(foreignName)
                        .IsRequired();
        }
    }
}