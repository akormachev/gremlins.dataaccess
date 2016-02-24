using System;

namespace Gremlins.DataAccess
{
    public static class DataReflectorCollectionExtension
    {
        public static DataReflectorCollection Add<TEntity, TDataReflector>(this DataReflectorCollection collection, TDataReflector reflector)
            where TDataReflector: DataReflector<TEntity>
        {
            if (collection == null)
                throw new NullReferenceException();

            collection.AddReflector<TEntity, TDataReflector>(reflector);
            return collection;
        }
    }
}
