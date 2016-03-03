using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gremlins.DataAccess
{
    public class DefaultParameterConfiguration<T> : ParameterConfiguration<T>
    {
        protected override void Apply(IDbDataParameter parameter, T value)
        {
            if (value == null)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = value;
        }
    }

    public class NDateTimeParameterConfiguration: DefaultParameterConfiguration<Nullable<DateTime>>
    {
        protected override void Apply(IDbDataParameter parameter, DateTime? value)
        {
            base.Apply(parameter, value);

            parameter.DbType = DbType.DateTime;
        }
    }
}
