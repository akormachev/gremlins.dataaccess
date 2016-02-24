using System.Collections.Generic;

namespace Gremlins.DataAccess.Samples.ChildTableSample
{
    class Customer
    {
        public string Address { get; set; }
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set;}
        public IList<Order> Orders { get; set; }
    }
}
