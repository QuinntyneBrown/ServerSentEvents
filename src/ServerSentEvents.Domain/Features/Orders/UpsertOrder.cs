using FluentValidation;
using MediatR;
using ServerSentEvents.Core.Data;
using ServerSentEvents.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Domain.Features.Orders
{
    public class UpsertOrder
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Order).NotNull();
                RuleFor(request => request.Order).SetValidator(new OrderValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public OrderDto Order { get; set; }
        }

        public class Response
        {
            public OrderDto Order { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IServerSentEventsDbContext _context;

            public Handler(IServerSentEventsDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var order = await _context.Orders.FindAsync(request.Order.OrderId);

                if (order == null)
                {
                    order = new Order();
                    await _context.Orders.AddAsync(order);
                }

                order.CustomerId = request.Order.CustomerId;
                order.Total = request.Order.Total;
                order.Modified = request.Order.Modified;

                await _context.SaveChangesAsync(cancellationToken);

			    return new Response() { 
                    Order = order.ToDto()
                };
            }
        }
    }
}
