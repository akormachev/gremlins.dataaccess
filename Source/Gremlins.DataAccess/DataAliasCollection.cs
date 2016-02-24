using System;
using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// Used to store alias for custom stored procedure column names to specific entity path
    /// </summary>
    public sealed class DataAliasCollection
    {

        #region Fields

        private readonly IDictionary<string, IDictionary<string, string>> _items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of <see cref="DataAliasCollection"/>
        /// </summary>
        public DataAliasCollection()
        {
            _items = new Dictionary<string, IDictionary<string, string>>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add mapping between database result and entity property path
        /// </summary>
        /// <param name="mapperName">Output cursor/table name</param>
        /// <param name="fieldName">Output field name</param>
        /// <param name="propertyPath">Entity property path</param>
        /// <returns>Mappings object</returns>
        public void AddMapping(string mapperName, string fieldName, string propertyPath)
        {
            if (string.IsNullOrEmpty(mapperName))
                throw new ArgumentNullException(nameof(mapperName));
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException(nameof(fieldName));
            if (string.IsNullOrEmpty(propertyPath))
                throw new ArgumentNullException(nameof(propertyPath));

            mapperName = mapperName.ToUpper();
            fieldName = fieldName.ToUpper();
            propertyPath = propertyPath.ToUpper();
            if (!_items.ContainsKey(mapperName))
                _items.Add(mapperName, new Dictionary<string, string>());
            if (_items[mapperName].ContainsKey(fieldName))
                _items[mapperName][fieldName] = propertyPath;
            else
                _items[mapperName].Add(fieldName, propertyPath);            
        }
        
        #endregion

        #region Internal methods

        /// <summary>
        /// Get mapping for database result
        /// </summary>
        /// <param name="mapperName">Output cursor/table name</param>
        /// <param name="fieldName">Output field name</param>
        /// <returns>Entity property path</returns>
        public string Get(string mapperName, string fieldName)
        {
            if (!_items.ContainsKey(mapperName))
                return fieldName;
            if (!_items[mapperName].ContainsKey(fieldName))
                return fieldName;
            return _items[mapperName][fieldName];
        }

        #endregion

    }
}
