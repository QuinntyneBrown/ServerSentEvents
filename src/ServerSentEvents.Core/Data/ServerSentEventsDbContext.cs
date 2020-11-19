using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServerSentEvents.Core.Models;

namespace ServerSentEvents.Core.Data
{
    public class ServerSentEventsDbContext: DbContext, IServerSentEventsDbContext
    {
        public ServerSentEventsDbContext(DbContextOptions options)
            :base(options) { }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Order> Orders { get; private set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
