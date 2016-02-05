using System;
using Microsoft.ServiceBus.Messaging;

namespace Client.Service.WebApi.Controllers
{
    using Microsoft.AspNet.Mvc;

    [Route("checkoutOrder")]
    public class CheckoutOrderController : Controller
    {
        private readonly QueueClient queue_client;

        public CheckoutOrderController(QueueClient queueClient)
        {
            if (queueClient == null)
                throw new ArgumentNullException(nameof(queueClient));

            queue_client = queueClient;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            queue_client.Send(new BrokeredMessage(new {id = Guid.NewGuid().ToString("D"), Order = value}));

            Ok();
        }
    }
}
