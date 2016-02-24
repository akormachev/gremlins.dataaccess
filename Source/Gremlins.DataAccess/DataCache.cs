using System;
using System.Collections;
using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    class DataCache
    {
        private readonly IDictionary<string, DataCacheItem> _items;

        public DataCache()
        {
            _items = new Dictionary<string, DataCacheItem>();
        }

        public void Register(Type itemType, Type listType, string path)
        {
            if (!_items.ContainsKey(path))
                _items.Add(path, new DataCacheItem { Type = itemType, Data = (IList)Activator.CreateInstance(listType) });
        }

        public void Add<T>(string path, T entity)
        {
            if (!_items.ContainsKey(path))
                _items.Add(path, new DataCacheItem { Type = typeof(T), Data = new List<T>() });
            _items[path].Data.Add(entity);
        }

        public Type ItemType(string path)
        {
            if (_items.ContainsKey(path))
                return _items[path].Type;
            return null;
        }

        public IEnumerable Get(string path)
        {
            if (_items.ContainsKey(path))
                return _items[path].Data;
            return null;
        }

        public IEnumerable<TEntity> Get<TEntity>(string path)
        {
            return Get(path) as List<TEntity>;
        }
    }

    class DataCacheItem
    {
        public Type Type { get; set; }
        public IList Data { get; set; }        
    }
}