using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServerSentEvents.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public override Int32 SaveChanges()
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess: true);
        }

        public override Int32 SaveChanges(Boolean acceptAllChangesOnSuccess)
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess);
        }
        public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        }
        public override Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
