using System;

namespace Gremlins.DataAccess
{
    public static class InputCollectionExtension
    {

        #region Private methods

        private static InputCollection Add(this InputCollection collection, string name, object value, ParameterConfiguration factory = null)
        {
            if (collection == null)
                throw new NullReferenceException("collection is null");

            collection.AddParameter(name, value, factory);

            return collection;
        }

        private static InputCollection AddIf(this InputCollection collection, bool condition, string name, object value, ParameterConfiguration factory = null)
        {
            if (collection == null)
                throw new NullReferenceException("collection is null");

            collection.AddParameterIf(condition, name, value, factory);

            return collection;
        }

        #endregion

        public static InputCollection Add<T>(this InputCollection collection, string name, T value, ParameterConfiguration<T> factory)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, bool value, ParameterConfiguration<bool> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }


		public static InputCollection Add(this InputCollection collection, string name, decimal value, ParameterConfiguration<decimal> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, decimal? value, ParameterConfiguration<decimal?> factory = null)
        {
            return Add(collection, name, (object)value, factory ??  new NullableParameterConfiguration<decimal>());
        }

        public static InputCollection Add(this InputCollection collection, string name, DateTime value, ParameterConfiguration<DateTime> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, DateTime? value, ParameterConfiguration<DateTime?> factory = null)
        {
            if (factory == null)
                factory = new NullableParameterConfiguration<DateTime>();
            return Add(collection, name, (object)value, factory);
            
        }        

        public static InputCollection Add(this InputCollection collection, string name, float value, ParameterConfiguration<float> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, Guid value, ParameterConfiguration<Guid> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, int value, ParameterConfiguration<int> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, long value, ParameterConfiguration<long> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, short value, ParameterConfiguration<short> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

        public static InputCollection Add(this InputCollection collection, string name, string value, ParameterConfiguration<string> factory = null)
        {
            return Add(collection, name, (object)value, factory);
        }

		public static InputCollection Add(this InputCollection collection, string name, DBNull value)
		{
			return Add(collection, name, (object)value);
		}

    }
}
