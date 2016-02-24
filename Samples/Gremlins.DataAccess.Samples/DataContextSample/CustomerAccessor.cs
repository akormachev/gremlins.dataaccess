using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gremlins.DataAccess.Samples.DataContextSample
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

        public int Save(Customer customer)
        {
            return Execute("CreateCustomer",
                InputCollection.Create()
                    .Add("CustomerID", customer.CustomerID)
                    .Add("CompanyName", customer.CompanyName)
                    .Add("ContactName", customer.ContactName)
                    .Add("ContactTitle", customer.ContactTitle)
                    .Add("Address", customer.Address));
        }
    }
}
