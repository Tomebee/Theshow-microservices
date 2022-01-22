using System.Threading;
using System.Threading.Tasks;
using Application.Core.Commands.MakeReservation;
using Application.Core.Queries.GetUserReservations;
using Application.IntegrationEvents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit.vNext.Disposable;
using TheShow.Core.Extensions;

namespace TheShow.Core.Controllers
{
    [ApiController]
    [Route("api/1.0/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReservationsForCurrentUser(CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(new GetUserReservationsQuery
            {
                RequestedUserReservationId = User.Id()
            }, cancellationToken));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeReservation([FromBody] MakeReservationCommand command, CancellationToken cancellationToken = default)
        {
            command.UserId = User.Id();
            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
