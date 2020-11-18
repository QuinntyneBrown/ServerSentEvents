using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerSentEvents.Domain.Features.Orders;
using System.Net;
using System.Threading.Tasks;

namespace ServerSentEvents.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpPost(Name = "UpsertOrderRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertOrder.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertOrder.Response>> Upsert([FromBody]UpsertOrder.Request request)
            => await _mediator.Send(request);

        [HttpDelete("{orderId}", Name = "RemoveOrderRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveOrder.Request request)
            => await _mediator.Send(request);

        [HttpGet("{orderId}", Name = "GetOrderByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOrderById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetOrderById.Response>> GetById([FromRoute]GetOrderById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Order == null)
            {
                return new NotFoundObjectResult(request.OrderId);
            }

            return response;
        }

        [HttpGet(Name = "GetOrdersRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOrders.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetOrders.Response>> Get()
            => await _mediator.Send(new GetOrders.Request());           
    }
}
