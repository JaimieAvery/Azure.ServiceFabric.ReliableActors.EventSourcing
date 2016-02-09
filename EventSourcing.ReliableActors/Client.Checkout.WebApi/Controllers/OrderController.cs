namespace Client.Checkout.WebApi.Controllers
{
    using System;
    using Contracts;
    using Microsoft.ServiceBus.Messaging;

    using Microsoft.AspNet.Mvc;

    [Route("api")]
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
        public void Post([FromBody]string value)
        {
            queue_client.Send(new BrokeredMessage(new PlaceOrder(value).ToJson()));

            Ok();
        }
    }
}
