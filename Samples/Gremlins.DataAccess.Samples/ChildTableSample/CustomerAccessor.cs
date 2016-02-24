using System.Data;
using System.Linq;

namespace Gremlins.DataAccess.Samples.ChildTableSample
{
    class CustomerAccessor: EntityAccessor<Customer>
    {
        public CustomerAccessor(IDbConnection connection)
            :base(connection)
        {
            UsingMapper<Customer, CustomerMapper>();
            UsingMapper<Order, OrderMapper>();
        }

        public Customer Get(string customerID)
        {
            return Execute("GetCustomer",
                InputCollection.Create()
                    .Add("CustomerID", customerID),
                OutputCollection.Create()
                    .Add("Customers")
                    .Add("Customers#Orders"))
                .FirstOrDefault();
        }        
    }
}
