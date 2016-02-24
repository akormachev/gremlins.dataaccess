using System;
using System.Data.SqlClient;

namespace Gremlins.DataAccess.Samples.ChildTableSample
{
    static class Sample
    {
        public static void Run()
        {
            try
            {

                using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString))
                {
                    connection.Open();

                    var accessor = new CustomerAccessor(connection);

                    var customer = accessor.Get("HANAR");
                    if (customer == null)
                        return;

                    Program.InfoLine($" * {customer.CustomerID,1} {customer.CompanyName}");

                    foreach (var order in customer.Orders)
                        Program.InfoLine($"   ** {order.OrderID,5} {order.CustomerID,5} {order.OrderDate}");
                        
                }
            }
            catch (Exception ex)
            {
                Program.ErrorMessage("Error during sample run.");
                Program.ErrorMessage(ex.ToString());
            }
        }
    }
}
