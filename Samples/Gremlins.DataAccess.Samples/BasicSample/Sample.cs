using System;
using System.Data.SqlClient;

namespace Gremlins.DataAccess.Samples.BasicSample
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

                    var customers = accessor.Select();

                    foreach (var customer in customers)
                        Program.InfoLine($" * {customer.CustomerID,1} {customer.CompanyName,10}");
                        
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
