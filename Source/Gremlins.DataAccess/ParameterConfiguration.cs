using System.Data;
using System.Data.Common;

namespace Gremlins.DataAccess
{
    public abstract class ParameterConfiguration
    {
        protected internal abstract void Apply(IDbDataParameter parameter, object value);
    }
}
