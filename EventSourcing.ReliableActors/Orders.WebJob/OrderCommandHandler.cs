using System;
using System.Collections.Generic;
using Contracts;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace Orders.WebJob
{
    public static class OrderCommandHandler
    {
        public static void HandlePlaceOrder(
            [ServiceBusTrigger("orders-commands")] PlaceOrder command/*,
            [ServiceBus("orders-events")] ICollector<BrokeredMessage> @events*/)
        {

            Console.Out.WriteLine(command);
            // @events.Add(new BrokeredMessage());
        }
    }
}
