namespace Gremlins.DataAccess
{
    public class InputParameter
    {
        public InputParameter(string name, object value, ParameterConfiguration factory = null)
        {
            this.Name = name;
            this.Value = value;
            this.Configuration = factory;
        }

        public string Name { get; }
        public object Value { get; }
        public ParameterConfiguration Configuration { get; }
    }
}
