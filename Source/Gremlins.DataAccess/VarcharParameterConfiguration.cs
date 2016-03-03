using System.Data;

namespace Gremlins.DataAccess
{
    public class VarcharParameterConfiguration: DefaultParameterConfiguration<string>
    {
        public VarcharParameterConfiguration(int size)
        {
            this.Size = Size;
        }

        public int Size { get; }

        protected override void Apply(IDbDataParameter parameter, string value)
        {
            base.Apply(parameter, value);

            parameter.Size = Size;
        }
    }
}
