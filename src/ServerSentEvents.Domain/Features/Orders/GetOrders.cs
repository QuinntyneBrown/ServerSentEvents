using MediatR;
using Microsoft.EntityFrameworkCore;
using ServerSentEvents.Core.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Domain.Features.Orders
{
    public class GetOrders
    {
        public class Request : IRequest<Response> {  }

        public class Response
        {
            public List<OrderDto> Orders { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IServerSentEventsDbContext _context;

            public Handler(IServerSentEventsDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    Orders = await _context.Orders.Select(x => x.ToDto()).ToListAsync()
                };
            }
        }
    }
}
