using System;
using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// Key structure of MappingCollection
    /// </summary>
    internal struct MappingPath
    {
        public string Path { get; internal set; }

        public Type Type { get; internal set; }
    }

    internal class EntityMappingCollection
    {
        #region Fields

        private IDictionary<MappingPath, EntityMapping> _items;

        #endregion Fields

        #region Constructors

        public EntityMappingCollection()
        {
            _items = new Dictionary<MappingPath, EntityMapping>();
        }

        #endregion Constructors

        #region Internal methods

        internal void AddMapping(Type declaringType, EntityMapping mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException("mapping", Properties.Resources.MappingNotSpecified);
            if (mapping.Name == null)
                throw new ArgumentNullException("mapping.Name", Properties.Resources.MappingNotSpecified);

            var path = new MappingPath { Type = declaringType, Path = mapping.Name };

            if (_items.ContainsKey(path))
                throw new ArgumentException(Properties.Resources.DuplicatePartName, "part");

            _items.Add(path, mapping);
        }

        internal EntityMapping GetMapping(Type declaringType, string path)
        {
            var mappingPath = new MappingPath { Type = declaringType, Path = path };

            if (_items.ContainsKey(mappingPath))
                return _items[mappingPath];
            return null;
        }

        #endregion Internal methods

    }
}