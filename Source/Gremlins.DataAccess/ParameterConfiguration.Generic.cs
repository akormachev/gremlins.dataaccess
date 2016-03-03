using System;
using System.Data;

namespace Gremlins.DataAccess
{
    public abstract class ParameterConfiguration<T>: ParameterConfiguration
    {

        public ParameterConfiguration()
        {
            Direction = ParameterDirection.Input;
        }

        protected internal override void Apply(IDbDataParameter parameter, object value)
        {
            this.Parameter = parameter;

            this.Parameter.Direction = Direction;            

            Apply(parameter, (T)value);
        }

        protected abstract void Apply(IDbDataParameter parameter, T value);

        public ParameterDirection Direction { get; set; }

        protected IDbDataParameter Parameter { get; private set; }

        public virtual T Value
        {
            get
            {
                if (Parameter == null)
                    return default(T);

                if (Parameter.Value == DBNull.Value)
                    return default(T);

                return (T)Parameter.Value;
            }
        }
    }
}
