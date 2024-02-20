using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealPlaza.Authentication.Application.Features.Login.Commands.Registrar;
using RealPlaza.Authentication.Application.Features.Login.Queries.IniciarSesion;

namespace RealPlaza.Authentication.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;

        public LoginController(IConfiguration configuration, IMediator mediator)
        {
            this.configuration = configuration;
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<ActionResult> Registrar([FromBody] RegistrarCommand command)
        {
            return Ok(await mediator.Send(command));
           
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> Auth([FromBody]  IniciarSesionQuery command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
