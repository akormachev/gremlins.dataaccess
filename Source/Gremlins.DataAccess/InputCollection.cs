using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    public class InputCollection : IEnumerable<InputParameter>
    {

        #region Static fields

        private static InputCollection _empty;

        #endregion

        #region Static constructors

        static InputCollection()
        {
            _empty = new InputCollection();
        }

        #endregion

        #region Static properties

        public static InputCollection Empty { get { return _empty; } }

        #endregion

        #region Static methods

        public static InputCollection Create()
        {
            return new InputCollection();
        }        

        public static InputCollection Create(params InputParameter[] parameters)
        {
            var result = new InputCollection();
            foreach (var parameter in parameters)
                result.AddParameter(parameter.Name, parameter.Value, parameter.Configuration);
            return result;
        }

	    public static InputCollection Create(IDictionary<string, object> parameters)
	    {
			var result = new InputCollection();
			foreach (var parameter in parameters)
				result.AddParameter(parameter.Key, parameter.Value, null);
			return result;
	    }

	    #endregion

        #region Fields

        private readonly List<InputParameter> _items;

        #endregion

        #region Constructors

        private InputCollection() 
        {
            _items = new List<InputParameter>();
        }

        #endregion

        #region Public methods
        
        public void AddParameter(string name, object value, ParameterConfiguration factory = null)
        {           
            _items.Add(new InputParameter(name, value, factory));            
        }

        public void AddParameterIf(bool condition, string name, object value, ParameterConfiguration factory = null)
        {
            if (condition)
                AddParameter(name, value, factory);            
        }

        public IEnumerator<InputParameter> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        #region Private methods

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion
    }
}

