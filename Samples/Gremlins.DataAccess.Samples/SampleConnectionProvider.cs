using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Gremlins.DataAccess.Samples
{
    class SampleConnectionProvider : IDbConnectionProvider
    {
        private readonly ConnectionStringSettingsCollection _connectionSettings;

        public SampleConnectionProvider()
        {
            var connectionSettings = ConfigurationManager.ConnectionStrings;
            if (connectionSettings == null)
                throw new ArgumentNullException("connectionStrings");
            _connectionSettings = connectionSettings;
        }

        public IDbConnection Get(string name)
        {
            var connectionSettings = _connectionSettings[name];
            if (connectionSettings == null)
                throw new KeyNotFoundException(string.Format("Connection not found. ConnectionName = \"{0}\"", name));

            var factory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);

            var connection = factory.CreateConnection();

            connection.ConnectionString = connectionSettings.ConnectionString;

            return connection;
        }
    }
}
