using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gremlins.DataAccess.Samples.DataContextSample
{
    class CustomerMapper : EntityMapper<Customer>
    {
        protected override Customer Read(DataRecord record, string path)
        {
            return new Customer
            {
                Address = record.String(path, nameof(Customer.Address)),
                ContactName = record.String(path, nameof(Customer.ContactName)),
                ContactTitle = record.String(path, nameof(Customer.ContactTitle)),
                CustomerID = record.String(path, nameof(Customer.CustomerID)),
                CompanyName = record.String(path, nameof(Customer.CompanyName))
            };
        }
    }
}
