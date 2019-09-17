using DA;
using DACommon;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace BL
{
    public abstract class BaseBL<BT, DT>
        where BT : Entities.BaseEntity
        where DT : DACommon.Entities.BaseEntity
    {
        private const string BLFilter = "BL.Entities";
        private const string DAFilter = "DACommon.Entities";
        protected IBaseRepository<DT> currentRepository;
        protected BaseBL(IUnitOfWork unitOfWork)
        {

        }

        public BT[] GetAll()
        {
            return currentRepository.GetQuery().ToList()
                 .Select(c => MapToBLEntity(c)).ToArray();
        }
        public virtual void Add(BT entity)
        {
            currentRepository.Insert(MapToDAEntity(entity));
            currentRepository.SaveChanges();
        }
        protected BT MapToBLEntity(DT entity)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            BT newEntity = FillProperty<DT, BT>(entity, BLFilter, assembly);
            return newEntity;
        }

        protected DT MapToDAEntity(BT entity)
        {
            return MapToDAEntity<BT, DT>(entity);
        }

        protected K MapToDAEntity<T, K>(T entity)
        {
            Assembly assembly = Assembly.Load("DACommon");
            K newEntity = FillProperty<T, K>(entity, DAFilter, assembly);
            return newEntity;
        }
        protected K FillProperty<S, K>(S entity, string filter, Assembly assembly)
        {
            object newEntity = assembly.CreateInstance(typeof(K).FullName);
            foreach (var property in entity.GetType().GetProperties())
            {
                var newPerop = newEntity.GetType().GetProperty(property.Name);
                if (newPerop != null)
                {
                    if (newPerop.PropertyType.Namespace.Contains(filter))
                    {
                        var fillPropertyMethod = this.GetType().GetMethod("FillProperty", BindingFlags.Instance | BindingFlags.NonPublic);
                        fillPropertyMethod = fillPropertyMethod.MakeGenericMethod(property.PropertyType, newPerop.PropertyType);

                        var methodParam = new ArrayList(3);
                        methodParam.Add(property.GetValue(entity));
                        methodParam.Add(filter);
                        methodParam.Add(assembly);

                        var newValue = fillPropertyMethod.Invoke(this, methodParam.ToArray());
                        newPerop.SetValue(newEntity, newValue);
                    }
                    else
                        newPerop.SetValue(newEntity, property.GetValue(entity));
                }
            }
            return (K)newEntity;
        }

    }
}