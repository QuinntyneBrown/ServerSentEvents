using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServerSentEvents.Api.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Api.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController: Controller
    {
        private readonly INotificationService _notificationService;
        public EventsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("queue")]
        public async Task Queue(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            _notificationService.Subscribe(async e =>
            {
                var orders = JsonConvert.SerializeObject(e);

                await response
                .WriteAsync($"data: {orders}\r\r");

                response.Body.Flush();

            });

            await tcs.Task;

        }

    }
}
