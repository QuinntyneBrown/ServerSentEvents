using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ServerSentEvents.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController: Controller
    {
        [HttpGet]
        public async Task Get()
        {
            var response = Response;

            response.Headers.Add("Content-Type", "text/event-stream");

            for (var i = 0; true; ++i)
            {
                await response
                    .WriteAsync($"data: Controller {i} at {DateTime.Now}\r\r");

                response.Body.Flush();
                await Task.Delay(5 * 1000);
            }
        }
    }
}
