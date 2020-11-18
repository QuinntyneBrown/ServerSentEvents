using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServerSentEvents.Core.Data;
using ServerSentEvents.Core.Models;
using System.IO;
using System.Threading.Tasks;

namespace ServerSentEvents.Api.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController: Controller
    {
        [HttpGet]
        public async Task Get()
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            Triggers<Order, ServerSentEventsDbContext>.Inserting += async (e) => {

                var json = JsonConvert.SerializeObject(new
                {
                    orderId = e.Entity.OrderId,
                    customerId = e.Entity.CustomerId,
                    total = e.Entity.Total
                });

                await response
                    .WriteAsync($"data: {json}\r\r");

                response.Body.Flush();

            };

            await tcs.Task;
        }
    }
}
