using System.Data;

namespace Gremlins.DataAccess
{
    public interface IDbConnectionProvider
    {
        IDbConnection Get(string name);
    }
}
