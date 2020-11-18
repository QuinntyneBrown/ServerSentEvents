using System;

namespace ServerSentEvents.Domain.Features.Orders
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
    }
}
