using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gremlins.DataAccess.Samples.ChildTableSample
{
    class Order
    {
        public string CustomerID { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public int OrderID { get; internal set; }
        public string ShipName { get; internal set; }
    }
}
