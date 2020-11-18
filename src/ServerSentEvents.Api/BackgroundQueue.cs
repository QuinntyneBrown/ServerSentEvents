using ServerSentEvents.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Api
{
    public interface IBackgroundQueue
    {
        void Queue(List<Order> orders);

        Task<List<Order>> DequeueAsync(
            CancellationToken cancellationToken);
    }

    public class BackgroundQueue : IBackgroundQueue
    {
        private ConcurrentQueue<List<Order>> _orders =
            new ConcurrentQueue<List<Order>>();

        public void Queue(
            List<Order> orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }

            _orders.Enqueue(orders);

        }

        public async Task<List<Order>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            _orders.TryDequeue(out var orders);

            return orders;
        }
    }
}
