using System.Data;

namespace Gremlins.DataAccess
{
    public abstract class ParameterConfiguration<T>: ParameterConfiguration
    {
        protected internal override void Apply(IDbDataParameter parameter, object value)
        {
            Apply(parameter, (T)value);
        }

        protected abstract void Apply(IDbDataParameter parameter, T value);
    }
}
