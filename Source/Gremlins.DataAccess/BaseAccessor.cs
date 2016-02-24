using System.Data;

namespace Gremlins.DataAccess
{
    /// <summary>
    /// Most general and abstract Accessor object
    /// </summary>
    public abstract class BaseAccessor
    {

        #region Fields

        private readonly IDbConnection _connection;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets database connection
        /// </summary>
        protected IDbConnection Connection
        {
            get { return _connection; }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Create an instance of <see cref="BaseAccessor"/>
        /// </summary>
        /// <param name="instance">Database connection</param>
        public BaseAccessor(IDbConnection connection)
        {
            _connection = connection;
        }

        #endregion

    }
}
