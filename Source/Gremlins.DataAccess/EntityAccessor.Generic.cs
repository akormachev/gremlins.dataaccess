using System.Collections.Generic;
using System.Data;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityAccessor<T> : EntityAccessor
	{

		#region Constructors

		/// <summary>
		/// Create an instance of <see cref="EntityAccessor"/>
		/// </summary>
		public EntityAccessor(IDbConnection connection)
			: base(connection)
		{ }

		#endregion

		#region Public methods

		public IEnumerable<T> Execute(string commandName, InputCollection inputCollection, OutputCollection outputCollection, EntityMapperCollection specialMappers = null, DataReflectorCollection specialReflectors = null, DataAliasCollection settings = null)
		{
			return base.Execute<T>(commandName, inputCollection, outputCollection, specialMappers, specialReflectors, settings);
		}

		#endregion

		#region Private methods

		#endregion

	}
}
