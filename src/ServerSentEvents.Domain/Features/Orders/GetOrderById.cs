using MediatR;
using ServerSentEvents.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Domain.Features.Orders
{
    public class GetOrderById
    {
        public class Request : IRequest<Response> {  
            public Guid OrderId { get; set; }        
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
			    return new Response() { 
                    Order = (await _context.Orders.FindAsync(request.OrderId)).ToDto()
                };
            }
        }
    }
}
