namespace Contracts
{
    using System;

    public class PlaceOrder : ICommand
    {
        public PlaceOrder(Guid orderId, string customerId, string bagId)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            BagId = bagId;
            CustomerId = customerId;
        }

        public Guid Id { get; }

        public Guid OrderId { get; }

        public string BagId { get; }

        public string CustomerId { get; }
    }
}
