namespace Gremlins.DataAccess
{
    public static class OutputCollectionExtension
    {
        #region Public methods

        public static OutputCollection Add(this OutputCollection collection, string key)
        {
            collection.AddParameter(key);
            return collection;
        }

        public static OutputCollection AddIf(this OutputCollection collection, bool condition, string key)
        {            
            if (condition)
                collection.AddParameter(key);
            return collection;
        }

        #endregion
    }
}
