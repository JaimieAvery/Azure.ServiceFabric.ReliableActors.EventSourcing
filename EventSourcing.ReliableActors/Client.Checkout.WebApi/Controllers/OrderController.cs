namespace Client.Checkout.WebApi.Controllers
{
    using System;
    using Contracts;
    using Microsoft.ServiceBus.Messaging;

    using Microsoft.AspNet.Mvc;

    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly QueueClient queue_client;

        public OrderController(QueueClient queueClient)
        {
            if (queueClient == null)
                throw new ArgumentNullException(nameof(queueClient));

            queue_client = queueClient;
        }

        [HttpPost]
        public void Post([FromBody]Order order)
        {
            var message = new BrokeredMessage(new PlaceOrder(order.OrderId, order.CustomerId, order.BagId).ToJson())
            {
                ContentType = "application/json"
            };
            queue_client.Send(message);

            Ok();
        }
    }

    public class Order
    {
        public Guid OrderId { get; set; }

        public string BagId { get; set; }

        public string CustomerId { get; set; }
    }
}
