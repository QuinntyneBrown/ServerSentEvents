using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerSentEvents.Api.Services;
using ServerSentEvents.Core.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Api
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public NotificationBackgroundService(INotificationService notificationService, IServiceScopeFactory serviceScopeFactory)
        {
            _notificationService = notificationService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public DateTime LastModified { get; set; }

        public int Interval { get; set; } = 5 * 1000;
        protected async override Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ServerSentEventsDbContext>();

                    var lastModifiedOrder = context.Orders.OrderByDescending(x => x.Modified).FirstOrDefault();

                    if(lastModifiedOrder != default  && lastModifiedOrder.Modified != this.LastModified)
                    {
                        LastModified = lastModifiedOrder.Modified;

                        _notificationService.OnNext(context.Orders.ToList());
                    }                    
                }

                await Task.Delay(this.Interval);
            }
        }
    }
}
