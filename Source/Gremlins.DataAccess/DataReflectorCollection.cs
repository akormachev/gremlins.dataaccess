using System;
using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    public sealed class DataReflectorCollection
    {
        #region Fields

        private IDictionary<Type, DataReflector> _items;

        #endregion

        #region Constructors

        internal DataReflectorCollection()
        {
            _items = new Dictionary<Type, DataReflector>();
        }

        internal DataReflectorCollection(DataReflectorCollection items)
            :this()
        {            
            foreach (var item in items._items)
                _items.Add(item);
        }

        #endregion

        #region Public methods

        public void AddReflector<TEntity, TEntityMapper>(TEntityMapper reflector)
            where TEntityMapper: DataReflector<TEntity>
        {
            if (_items.ContainsKey(typeof(TEntity)))
                throw new ArgumentException(Properties.Resources.DuplicateEntityType);
            _items.Add(typeof(TEntity), reflector);
        }

        internal void AddRange(DataReflectorCollection items)
        {
            foreach (var item in items._items)
                if (_items.ContainsKey(item.Key))
                    _items[item.Key] = item.Value;
                else
                    _items.Add(item);
        }

        public DataReflector Get(Type type)
        {
            if (_items.ContainsKey(type))
                return _items[type];
            return null;
        }

        public DataReflector<T> Get<T>()
        {
            return Get(typeof(T)) as DataReflector<T>;
        }        

        #endregion

        #region Static methods

        public static DataReflectorCollection Create()
        {
            return new DataReflectorCollection();
        }

        #endregion
    }
}
