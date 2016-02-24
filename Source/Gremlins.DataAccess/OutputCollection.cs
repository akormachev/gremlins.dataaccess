using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    public class OutputCollection : List<string>
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

        #region Constructors

        private OutputCollection() { }

        private OutputCollection(params string[] results)
        {
            this.AddRange(results);
        }

        #endregion

        #region Public methods

        public OutputCollection Add(string key)
        {
            base.Add(key);
            return this;
        }

        public OutputCollection AddIf(bool condition, string key)
        {
            if (condition)
                return this.Add(key);
            return this;
        }

        #endregion

    }
}
