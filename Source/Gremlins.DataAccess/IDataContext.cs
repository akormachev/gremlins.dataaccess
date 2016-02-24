using System;

namespace Gremlins.DataAccess
{
    public interface IDataContext : IDisposable
    {
        void SaveChanges();

        TAccessor Set<TAccessor>();
    }
}
