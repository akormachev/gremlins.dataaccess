using System.Collections.Generic;
using System.Data;

namespace Gremlins.DataAccess.Samples.BasicSample
{
    class CustomerAccessor: EntityAccessor<Customer>
    {
        public CustomerAccessor(IDbConnection connection)
            :base(connection)
        {
            UsingMapper<Customer, CustomerMapper>();
        }

        public IEnumerable<Customer> Select()
        {
            return Execute("SelectCustomers",
                InputCollection.Create(),
                OutputCollection.Create()
                    .Add("Customers"));
        }
    }
}
