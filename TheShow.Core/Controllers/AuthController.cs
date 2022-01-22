using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Core;
using Application.Core.Commands.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheShow.Core.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/1.0/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _mediator.Send(command, cancellationToken));
            }
            catch (CommandProcessingException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
