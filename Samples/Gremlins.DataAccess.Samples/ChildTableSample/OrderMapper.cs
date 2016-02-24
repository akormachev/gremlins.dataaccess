using System;

namespace Gremlins.DataAccess.Samples.ChildTableSample
{
    class OrderMapper : EntityMapper<Order>
    {
        protected override Order Read(DataRecord record, string path)
        {
            return new Order
            {
                OrderID = record.Int32(path, nameof(Order.OrderID)),
                CustomerID = record.String(path, nameof(Order.CustomerID)),
                ShipName = record.String(path, nameof(Order.ShipName)),
                OrderDate = record.Date(path, nameof(Order.OrderDate))
            };
        }
    }
}
