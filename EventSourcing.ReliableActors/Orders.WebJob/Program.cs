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
                ServiceBusConnectionString = $"{ConfigurationManager.ConnectionStrings["AzureWebJobsServiceBus"].ConnectionString}"
            }).RunAndBlock();
        }
    }
}
