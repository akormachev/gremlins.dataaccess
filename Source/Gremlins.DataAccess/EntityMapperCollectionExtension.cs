using System;

namespace Gremlins.DataAccess
{
    public static class EntityMapperCollectionExtension
    {
        public static EntityMapperCollection Add<TEntity, TEntityMapper>(this EntityMapperCollection collection)
            where TEntityMapper: EntityMapper<TEntity>, new()
        {
            if (collection == null)
                throw new NullReferenceException();

            collection.AddMapper<TEntity, TEntityMapper>();
            return collection;
        }
    }
}
