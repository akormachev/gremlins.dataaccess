using System.Data;
using System.Data.Common;

namespace Gremlins.DataAccess
{
    public static class DbConnectionExtension
    {
        public static DbCommand CreateCommand(this DbConnection connection, string commandName)
        {
            var result = connection.CreateCommand();
            result.CommandType = CommandType.StoredProcedure;
            result.CommandText = commandName;
            return result;
        }
    }
}
