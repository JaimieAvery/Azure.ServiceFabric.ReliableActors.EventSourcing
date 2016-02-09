namespace Contracts
{
    using System;

    public class PlaceOrder : ICommand
    {
        public PlaceOrder(string value)
        {
            Value = value;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Value { get; }
    }
}
