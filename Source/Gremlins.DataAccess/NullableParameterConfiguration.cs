using System;
using System.Data;

namespace Gremlins.DataAccess
{
    public class NullableParameterConfiguration<TEntity>: ParameterConfiguration<TEntity?>        
        where TEntity: struct
    {
        protected override void Apply(IDbDataParameter parameter, TEntity? value)
        {
            if (value == null)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = value.Value;
        }
    }
}
