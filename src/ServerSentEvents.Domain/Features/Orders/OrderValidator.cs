using FluentValidation;

namespace ServerSentEvents.Domain.Features.Orders
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            
        }
    }
}
