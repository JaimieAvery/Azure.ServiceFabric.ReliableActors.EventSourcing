using System;

namespace Contracts
{
    public class OrderPlaced : IEvent
    {
        public OrderPlaced(string value)
        {
            Value = value;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Value { get; }
    }
}