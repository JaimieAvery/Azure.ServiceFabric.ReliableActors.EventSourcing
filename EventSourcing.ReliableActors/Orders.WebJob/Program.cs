using Microsoft.Azure.WebJobs;
using System.Configuration;

namespace Orders.WebJob
{
    class Program
    {
        public static void Main()
        {
            new JobHost(new JobHostConfiguration
            {
                ServiceBusConnectionString = $"{ConfigurationManager.ConnectionStrings["OrdersWebJobsServiceBus"].ConnectionString}",
                DashboardConnectionString = string.Empty
                
            }).RunAndBlock();
        }
    }
}
