using System;
using System.Data;
using System.Transactions;

namespace Gremlins.DataAccess
{
    public abstract class DataContext: IDataContext
    {

        #region Fields

        private IDbConnection _connection;
        private TransactionScope _transaction;

        #endregion

        #region Constructors

        protected DataContext(IDbConnectionProvider connectionProvider, string connectionName = "default")
        {                    
            _transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromMinutes(5) });
            _connection = connectionProvider.Get(connectionName);
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        #endregion

        #region Public methods

        public void SaveChanges()
        {
            _transaction.Complete();
        }

        public TAccessor Set<TAccessor>()
        {
            return (TAccessor) Activator.CreateInstance(typeof(TAccessor), _connection);
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false;      

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection.Dispose();
                    _transaction.Dispose();
                }

                _transaction = null;
                _connection = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
