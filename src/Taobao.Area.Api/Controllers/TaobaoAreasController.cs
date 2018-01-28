using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Taobao.Area.Api.Domain.Commands;

namespace Taobao.Area.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TaobaoAreasController : Controller
    {
        private readonly IMediator _mediator;

        public TaobaoAreasController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("build")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Build()
        {
            var command = new DownloadCommand();
            var result = await _mediator.Send(command);
            if (string.IsNullOrEmpty(result))
                return BadRequest();
            else
                return Ok(result);
        }

        //[Route("ship")]
        //[HttpPut]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> ShipOrder([FromBody]ShipOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        //{
        //    bool commandResult = false;
        //    if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
        //    {
        //        var requestShipOrder = new IdentifiedCommand<ShipOrderCommand, bool>(command, guid);
        //        commandResult = await _mediator.Send(requestShipOrder);
        //    }

        //    return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();

        //}

        //[Route("{orderId:int}")]
        //[HttpGet]
        //[ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetOrder(int orderId)
        //{
        //    try
        //    {
        //        var order = await _orderQueries
        //            .GetOrderAsync(orderId);

        //        return Ok(order);
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}

        //[Route("")]
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetOrders()
        //{
        //    var orders = await _orderQueries.GetOrdersAsync();

        //    return Ok(orders);
        //}

        //[Route("cardtypes")]
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CardType>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetCardTypes()
        //{
        //    var cardTypes = await _orderQueries
        //        .GetCardTypesAsync();

        //    return Ok(cardTypes);
        //}
    }
}
