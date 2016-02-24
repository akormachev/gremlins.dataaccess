namespace Gremlins.DataAccess.Samples.ChildTableSample
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

        protected override MappingCollection Setup()
        {
            return MappingCollection.Create()
                .List<Customer, Order>("Orders", (parent, childs) => parent.Orders = childs, (parent, child) => parent.CustomerID == child.CustomerID);
        }
    }
}
