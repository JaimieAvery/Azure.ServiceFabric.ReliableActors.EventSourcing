using System;
using System.Threading;
using Contracts;
using Microsoft.ServiceBus.Messaging;

namespace Client
{
    class Program
    {
        private const string connection = "Endpoint=sb://order-messages.servicebus.windows.net/;SharedAccessKeyName=client;SharedAccessKey=vvTmqj2AonsVHDG3yi5D9zmjWDPnLe2ZpGoQzgneXOY=;EntityPath=orders-commands";

        static void Main(string[] args)
        {
            start:
            Console.Out.WriteLine("Hit any key to send command...");
            Console.ReadKey();

            try
            {

                var client = QueueClient.CreateFromConnectionString(connection, "orders-commands");
                client.Send(new BrokeredMessage(new PlaceOrder(Guid.NewGuid(), "jaimie", "12345").ToJson()));
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex);
                Console.ReadKey();
            }
            Console.Clear();
            Console.Out.WriteLine("Sent");
            goto start;

        }
    }
}
