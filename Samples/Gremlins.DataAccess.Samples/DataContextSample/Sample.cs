using System;
using System.Linq;

namespace Gremlins.DataAccess.Samples.DataContextSample
{
    static class Sample
    {
        public static void Run()
        {
            try
            {
                using (var context = new SampleDataContext())
                {
                    var customers = context.Set<CustomerAccessor>().Select();
                    if (customers.Any(x => x.CustomerID == "NWTCH"))
                        return;

                    context.Set<CustomerAccessor>().Save(new Customer { CustomerID = "NWTCH", CompanyName = "Night's Watch", ContactName = "John Snow", ContactTitle = "Lord Commander", Address = "Castle Black, the Wall, Westeros" });

                    Program.InfoLine(" * One record successfully created.");

                    context.SaveChanges();
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
