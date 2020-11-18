using Microsoft.EntityFrameworkCore;
using ServerSentEvents.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Core.Data
{
    public interface IServerSentEventsDbContext
    {
        DbSet<Order> Orders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
