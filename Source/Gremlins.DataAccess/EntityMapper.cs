using System;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// Abstract entity reader
    /// </summary>
    public abstract class EntityMapper
    {
        /// <summary>
        /// Get acceptance of readed entity
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="path">Property path</param>
        /// <returns>Acceptance</returns>
        protected internal virtual bool CanRead(DataRecord record, string path)
        {
            return true;
        }

        /// <summary>
        /// Read entity from data record
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="path">Current property path</param>
        /// <returns>Entity</returns>
        internal abstract object ReadEntity(DataRecord record, string path);

        /// <summary>
        /// Predefine EntityMapper mapping rules
        /// </summary>
        /// <param name="setup">Setup object</param>
        internal virtual void SetupInternal(Type declaringType, EntityMappingCollection setup)
        {                           
            // Get EntityMapper mapping setup
            var mappings = Setup();
            if (mappings == null)
                throw new InvalidOperationException(Properties.Resources.MappingNotSpecified);

            // Add mappings
            foreach (var mapping in mappings)
                setup.AddMapping(declaringType, mapping);
        }

        /// <summary>
        /// Predefine EntityMapper mapping rules
        /// </summary>
        /// <param name="setup">Setup object</param>
        protected internal virtual MappingCollection Setup()
        {
            return MappingCollection.Empty;
        }

        /// <summary>
        /// Read object from data record
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="path">Current property path</param>
        /// <returns>Object</returns>
        internal abstract object TakeObject(DataRecord record, string path);


    }
}
