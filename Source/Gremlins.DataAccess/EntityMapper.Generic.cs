using System;
using System.Collections.Generic;
using System.Linq;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// Abstract generic entity reader. Root of any entity reader object.
    /// </summary>
    /// <typeparam name="T">Type of entity read by EntityMapper</typeparam>
    public abstract class EntityMapper<T> : EntityMapper
    {

        #region Fields

        private readonly IList<EntityMapper> _nestedMappers;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of <see cref="EntityMapper"/>
        /// </summary>
        protected EntityMapper()
        {
            _nestedMappers = new List<EntityMapper>();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Register new mapper as nested
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TMapper">Mapper type</typeparam>
        protected void AddNested<TEntity, TMapper>()
            where TMapper : EntityMapper<TEntity>, new()
            where TEntity : T
        {
            _nestedMappers.Add(Activator.CreateInstance<TMapper>());            
        }

        /// <summary>
        /// Register new mapper as nested
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TMapper">Mapper type</typeparam>
        /// <param name="mapper">Mapper</param>
        protected void AddNested<TEntity, TMapper>(TMapper mapper)
            where TMapper : EntityMapper<TEntity>
            where TEntity : T
        {
            _nestedMappers.Add(mapper);
        }

        /// <summary>
        /// Fill specified entity from data source
        /// </summary>
        /// <param name="entity">Entity to fill</param>
        /// <param name="record">Data source image</param>
        /// <param name="path">Current relative path</param>
        protected virtual void Fill(T entity, DataRecord record, string path)
        { }

        /// <summary>
        /// Read entity from data source
        /// </summary>
        /// <param name="record">Data source image</param>
        /// <param name="path">Current relative path</param>
        /// <returns>Entity</returns>
        protected abstract T Read(DataRecord record, string path);

        /// <summary>
        /// Predefine EntityMapper mapping rules
        /// </summary>
        /// <param name="setup">Setup object</param>
        internal override void SetupInternal(Type declaringType, EntityMappingCollection setup)
        {
            if (declaringType == null)
                declaringType = typeof(T);

            base.SetupInternal(declaringType, setup);

            foreach (var mapper in _nestedMappers)
                mapper.SetupInternal(declaringType, setup);
        }

        #endregion

        #region Internal methods

        internal override object ReadEntity(DataRecord record, string path)
        {
            var entity = TakeEntity(record, path);
            record.RegisterEntity<T>(entity, path);
            return entity;
        }

        internal T TakeEntity(DataRecord record, string path)
        {
            EntityMapper nestedMapper = _nestedMappers.FirstOrDefault(x => x.CanRead(record, path));
            if (nestedMapper == null)
                return Read(record, path);

            var entity = (T)nestedMapper.TakeObject(record, path);

            Fill(entity, record, path);

            return entity;
        }

        internal override object TakeObject(DataRecord record, string path)
        {
            return TakeEntity(record, path);
        }

        #endregion

    }
}
