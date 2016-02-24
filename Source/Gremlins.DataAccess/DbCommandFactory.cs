using System.Data.Common;

namespace Gremlins.DataAccess
{
    public abstract class DbCommandFactory
    {
        public abstract DbCommand CreateCommand(string commandName);
    }
}
