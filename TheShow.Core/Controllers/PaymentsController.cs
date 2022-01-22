using System;
using System.Threading.Tasks;
using Application.Core.Commands.CreatePaymentIntent;
using Application.Core.Commands.UpdatePaymentStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TheShow.Core.Controllers
{
    [ApiController]
    [Route("api/1.0/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{reservationId}")]
        //[Authorize]
        public async Task<IActionResult> CreateIntent([FromRoute] Guid reservationId)
        {
            var response = await _mediator.Send(new CreatePaymentIntentCommand
            {
                ReservationId = reservationId
            });

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIntentStatus([FromQuery] string intentId)
        {
            var command = new UpdatePaymentStatusCommand
            {
                IntentId = intentId
            };

            await _mediator.Publish(command);

            return NoContent();
        }
    }
}
