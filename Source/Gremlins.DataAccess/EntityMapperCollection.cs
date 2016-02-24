using System;
using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    public sealed class EntityMapperCollection
    {

        #region Fields

        private IDictionary<Type, EntityMapper> _items;

        #endregion

        #region Constructors

        internal EntityMapperCollection()
        {
            _items = new Dictionary<Type, EntityMapper>();
        }

        internal EntityMapperCollection(EntityMapperCollection items)
            :this()
        {            
            foreach (var item in items._items)
                _items.Add(item);
        }

        #endregion

        #region Public methods

        public void AddMapper<TEntity, TEntityMapper>()
            where TEntityMapper: EntityMapper<TEntity>, new()
        {
            if (_items.ContainsKey(typeof(TEntity)))
                throw new ArgumentException(Properties.Resources.DuplicateEntityType);
            _items.Add(typeof(TEntity), Activator.CreateInstance<TEntityMapper>());
        }

        public void AddMapper<TEntity, TEntityMapper>(TEntityMapper instance)
            where TEntityMapper: EntityMapper<TEntity>
        {
            if (instance == null)
                throw new ArgumentNullException(Properties.Resources.RegistrationOfNullMapper);
            if (_items.ContainsKey(typeof(TEntity)))
                throw new ArgumentException(Properties.Resources.DuplicateEntityType);
            _items.Add(typeof(TEntity), instance);
        }

        internal void AddRange(EntityMapperCollection items)
        {
            foreach (var item in items._items)
                if (_items.ContainsKey(item.Key))
                    _items[item.Key] = item.Value;
                else
                    _items.Add(item);
        }

        public EntityMapper Get(Type type)
        {
            if (_items.ContainsKey(type))
                return _items[type];
            return null;
        }

        public EntityMapper<T> Get<T>()
        {
            return Get(typeof(T)) as EntityMapper<T>;
        }        

        #endregion

        #region Internal methods

        internal EntityMappingCollection TakeMappings()
        {
            EntityMappingCollection collection = new EntityMappingCollection();
            foreach (var item in _items)
                item.Value.SetupInternal(null, collection);
            return collection;
        }

        #endregion

        #region Static methods

        public static EntityMapperCollection Create()
        {
            return new EntityMapperCollection();
        }

        #endregion


    }
}
