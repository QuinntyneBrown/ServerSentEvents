using ServerSentEvents.Core.Models;
using ServerSentEvents.Domain.Features.Orders;

namespace ServerSentEvents.Domain.Features
{
    public static class OrderExtensions
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                Total = order.Total
            };
        }
    }
}
