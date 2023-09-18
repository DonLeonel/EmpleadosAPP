using Application.Dtos;
using Application.Dtos.Inteface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Commands.Post.PostCredencial;
using static Application.CQRS.Commands.Put.UpdateCredencial;
using static Application.CQRS.Commands.Post.LogCredencial;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredencialesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CredencialesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("save")]
        public Task<IResponseDTO> postCredencial([FromBody]PostCredencialCommand credencialCommand)
        {
            return _mediator.Send(credencialCommand);
        }

        [HttpPost]
        [Route("log")]
        public Task<IResponseDTO> loginCredencial([FromBody] LogCredencialCommand logCredencialCommand)
        {
            return _mediator.Send(logCredencialCommand);
        }

        [HttpPut]
        [Route("put")]
        public Task<IResponseDTO> putCredencial([FromBody] UpdateCredencialCommand updateCredencialCommand)
        {
            return _mediator.Send(updateCredencialCommand);
        }
    }
}
