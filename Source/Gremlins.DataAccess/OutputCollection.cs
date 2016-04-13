using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    public class OutputCollection : IEnumerable<string>
    {

        #region Static methods

        public static OutputCollection Create()
        {
            return new OutputCollection();
        }

        public static OutputCollection Create(params string[] results)
        {
            return new OutputCollection(results);
        }

        #endregion

        #region Fields

        private readonly List<string> _items;

        #endregion

        #region Constructors

        private OutputCollection()
        {
            _items = new List<string>();
        }

        private OutputCollection(params string[] results)
        {
            _items = new List<string>(results);
        }

        #endregion

        #region Public methods

        public void AddParameter(string key)
        {
            _items.Add(key);
        }

        public void AddParameterIf(bool condition, string key)
        {
            if (condition)
                this.AddParameter(key);
        }

        public IEnumerator<string> GetEnumerator()
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
