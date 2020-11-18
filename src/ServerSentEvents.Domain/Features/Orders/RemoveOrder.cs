using MediatR;
using ServerSentEvents.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Domain.Features.Orders
{
    public class RemoveOrder
    {
        public class Request : IRequest<Unit> {  
            public Guid OrderId { get; set; }        
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IServerSentEventsDbContext _context;

            public Handler(IServerSentEventsDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {
                
                _context.Orders.Remove(await _context.Orders.FindAsync(request.OrderId));
                
                await _context.SaveChangesAsync(cancellationToken);			    
                
                return new Unit();
            }
        }
    }
}
