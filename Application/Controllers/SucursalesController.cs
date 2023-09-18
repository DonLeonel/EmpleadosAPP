using Application.Dtos;
using Application.Dtos.Inteface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Commands.Post.PostSucursal;
using static Application.CQRS.Queries.GetSucursal;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SucursalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getSucursales")]
        public Task<List<SucursalResp>> getSucursales()
        {
            return _mediator.Send(new GetSucursalQuery());
        }

        [HttpPost]
        [Route("SaveSucursal")]
        public Task<IResponseDTO> saveSucursal([FromBody] PostSucursalCommand post)
        {
            return _mediator.Send(post);
        }
    }
}
