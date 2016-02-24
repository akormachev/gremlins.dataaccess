using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// Base entity accessor class
    /// </summary>
    public class EntityAccessor : BaseAccessor
    {

        #region Fields

        /// <summary>
        /// Collection of pre-defined EntityMappers applied to every query executed by this EntityAccessor
        /// </summary>
        private readonly EntityMapperCollection _mappers;

        /// <summary>
        /// Collection of pre-defined EntityReflectors applied to every query executed by this EntityAccessor
        /// </summary>
        private readonly DataReflectorCollection _reflectors;

        #endregion

        #region Constructors

        /// <summary>
        /// Create instance of <see cref="EntityAccessor"/>
        /// </summary>
        public EntityAccessor(IDbConnection connection)
            : base(connection)
        {
            _mappers = new EntityMapperCollection();
            _reflectors = new DataReflectorCollection();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Execute SQL-instruction to invoke query without any result.
        /// </summary>
        /// <param name="commandName">Stored procedure name</param>
        /// <param name="inputParameters">Stored procedure input parameters</param>
        public virtual int Execute(string commandName, InputCollection inputParameters)
        {
            try
            {
                // Using db-command
                using (var command = Connection.CreateCommand())
                {
                    // Setup command 
                    command.CommandText = commandName;
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in inputParameters)
                        command.AddParameter(parameter);

                    // Execute non query                   
                    return command.ExecuteNonQuery();
                }
            }
            catch (DataLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataLayerException(Properties.Resources.DataLayerExceptionMessage, ex);
            }
        }

        /// <summary>
        /// Execute SQL-instruction.
        /// </summary>
        /// <param name="commandName">Stored procedure name</param>
        /// <param name="inputParameters">Stored procedure input parameters</param>
        /// <param name="outputParameters">Stored procedure output results</param>
        /// <param name="settings">Data mapping used to resolve entity property path</param>
        public virtual IEnumerable<T> Execute<T>(string commandName, InputCollection inputParameters, OutputCollection outputParameters, EntityMapperCollection specialMappers = null, DataReflectorCollection specialReflectors = null, DataAliasCollection settings = null, object bag = null)
        {
            try
            {
                // Specify output parameters
                if (outputParameters == null)
                    outputParameters = OutputCollection.Create();

                // Specify data map settings
                if (settings == null)
                    settings = new DataAliasCollection();

                // Aggregate reader collection
                var mappers = new EntityMapperCollection(_mappers);
                if (specialMappers != null)
                    mappers.AddRange(specialMappers);

                // Aggregate reflector collection
                var reflectors = new DataReflectorCollection(_reflectors);
                if (specialReflectors != null)
                    reflectors.AddRange(specialReflectors);

                // Evaluate mappings collection                
                EntityMappingCollection mappings = mappers.TakeMappings();

                // Using db-command
                using (var command = Connection.CreateCommand())
                {
                    // Setup command
                    command.CommandText = commandName;
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters in input collection
                    foreach (var parameter in inputParameters)
                        command.AddParameter(parameter);

                    // Execute query/non query
                    return ExecuteQuery<T>(command, outputParameters.ToArray(), settings, mappings, mappers, reflectors, bag);
                }

            }
            catch (DataLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataLayerException(Properties.Resources.DataLayerExceptionMessage, ex);
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Register EntityMapper for reading entity of T1 type to global EntityAccessor scope
        /// </summary>
        /// <typeparam name="T1">Entity type</typeparam>
        /// <typeparam name="T2">EntityMapper type</typeparam>        
        protected void UsingMapper<T1, T2>()
            where T2 : EntityMapper<T1>, new()
        {
            _mappers.AddMapper<T1, T2>();
        }

        /// <summary>
        /// Register EntityMapper for reading entity of T1 type to global EntityAccessor scope
        /// </summary>
        /// <typeparam name="T1">Entity type</typeparam>
        /// <typeparam name="T2">EntityMapper type</typeparam>   
        /// <param name="instance">Instance of EntityMapper</param>
        protected void UsingMapper<T1, T2>(T2 instance)
            where T2 : EntityMapper<T1>
        {
            _mappers.AddMapper<T1, T2>(instance);
        }

        /// <summary>
        /// Register EntityReflector for reading entity of T1 type to global EntityAccessor scope
        /// </summary>
        /// <typeparam name="T1">Entity type</typeparam>
        /// <typeparam name="T2">DataReflector type</typeparam>  
        /// <param name="reflector">DataReflector instance</param>
        protected void UsingReflector<T1, T2>(T2 reflector)
            where T2 : DataReflector<T1>
        {
            _reflectors.AddReflector<T1, T2>(reflector);
        }

        /// <summary>
        /// Register EntityReflector for reading entity of T1 type to global EntityAccessor scope
        /// </summary>
        /// <param name="collection"></param>
        protected void UsingReflectors(DataReflectorCollection collection)
        {
            _reflectors.AddRange(collection);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Execute query and return data map
        /// </summary>
        private IEnumerable<T> ExecuteQuery<T>(IDbCommand command, string[] outputParameters, DataAliasCollection settings, EntityMappingCollection mappings, EntityMapperCollection mappers, DataReflectorCollection reflectors, object bag)
        {
            // Get root path
            var rootPath = outputParameters[0];

            using (var reader = command.ExecuteReader())
            {
                var record = new DataRecord(reader, mappers, reflectors, bag);

                int index = 0;
                do
                {
                    try
                    {
                        // Set path
                        record.SetPath(outputParameters[index]);

                        // Substitute stored procedure field names to manual mapped field names
                        for (int i = 0; i < reader.FieldCount; i++)
                            record.AddField(settings.Get(outputParameters[index], reader.GetName(i)), i);

                        // Detect current iteration type                        
                        if (index == 0)
                        {
                            // Detect primary reader for map
                            EntityMapper<T> entityMapper = mappers.Get<T>();
                            if (entityMapper == null)
                                throw new DataLayerException(Properties.Resources.PrimaryMapperNotRegistered);

                            // Register types of current path in cache
                            record.InitCache(typeof(T), typeof(List<T>), outputParameters[index]);

                            // Read data from stream
                            //if (reader.HasRows)
                            while (reader.Read())
                                entityMapper.ReadEntity(record, outputParameters[index]);
                        }
                        else
                        {
                            // Take relative and ending path
                            var relativePath = GetRelativePath(outputParameters[index]);
                            var endingPath = GetEndingPath(outputParameters[index]);

                            // Determine target entity type path
                            var entityType = record.GetEntityType(relativePath);
                            if (entityType == null)
                                continue;

                            // Take mapping
                            var mapping = mappings.GetMapping(entityType, endingPath);
                            if (mapping == null)
                                throw new DataLayerException(string.Format(Properties.Resources.MappingNotSpecified, endingPath));

                            // Detect reader registered for specified path
                            EntityMapper entityMapper = mappers.Get(mapping.ChildType);
                            if (entityMapper == null)
                                continue;

                            // Register types of current path in cache
                            record.InitCache(mapping.ChildType, mapping.ChildListType, outputParameters[index]);

                            // Read data from stream and reflect it to primary entitiy
                            //if (reader.HasRows)
                            while (reader.Read())
                                entityMapper.ReadEntity(record, outputParameters[index]);

                            // Taking left entities
                            var parentEntities = record.TakeEntities(relativePath);
                            if (parentEntities == null)
                                break;

                            // Taking child entities
                            var childEntities = record.TakeEntities(outputParameters[index]);

                            // Invoke join operation
                            foreach (var parent in parentEntities)
                            {
                                if (!mapping.IsMatch(parent))
                                    continue;
                                List<object> result = new List<object>();
                                if (childEntities != null)
                                    foreach (var child in childEntities)
                                        if (mapping.JoinCondition(parent, child))
                                            result.Add(child);

                                if (result != null)
                                    mapping.Setter(parent, result);
                            }
                        }
                    }
                    finally
                    {
                        index++;
                    }
                }
                while (reader.NextResult());

                return record.TakeEntities<T>(rootPath);
            }
        }

        private static string GetEndingPath(string path)
        {
            int index = path.LastIndexOf('#');
            if (index == -1)
                throw new ArgumentException(Properties.Resources.SecondaryOutputParameterHaveToBeSplited);
            return path.Substring(index + 1);
        }

        private static string GetRelativePath(string path)
        {
            int index = path.LastIndexOf('#');
            if (index == -1)
                throw new ArgumentException(Properties.Resources.SecondaryOutputParameterHaveToBeSplited);
            return path.Substring(0, index);
        }

        #endregion

    }
}
