using System;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// Short notation extension to DataMapSettings class
    /// </summary>
    public static class DataAliasCollectionExtension
    {
        /// <summary>
        /// Add mapping between database result and entity property path
        /// </summary>
        /// <param name="collection">DataAliasCollection object</param>
        /// <param name="mapperName">Output cursor/table name</param>
        /// <param name="fieldName">Output field name</param>
        /// <param name="propertyPath">Entity property path</param>
        /// <returns>Mappings object</returns>
        public static DataAliasCollection Add(this DataAliasCollection collection, string mapperName, string fieldName, string propertyPath)
        {
            // Check input arguments
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            // Add mapping
            collection.AddMapping(mapperName, fieldName, propertyPath);
            // Return settings object
            return collection;
        }
    }
}
