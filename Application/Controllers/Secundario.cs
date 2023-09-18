using Application.Dtos;
using Application.Dtos.Inteface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Commands.Post.PostCargo;
using static Application.CQRS.Commands.Post.PostCiudad;
using static Application.CQRS.Queries.GetCiudades;
using static Applicationct.CQRS.Queries.GetCargos;

namespace ExamFinalProgII_Refact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Secundario : ControllerBase
    {
        private readonly IMediator _mediator;

        public Secundario(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("SaveCiudad")]
        public Task<IResponseDTO> saveCiudad([FromBody] PostCiudadCommand post)
        {
            return _mediator.Send(post);
        }

        [HttpPost]
        [Route("SaveCargo")]
        public Task<IResponseDTO> saveCargo([FromBody] PostCargoCommand post)
        {
            return _mediator.Send(post);
        }
        

        [HttpGet]
        [Route("getCargos")]
        public Task<List<CargoResp>> getCargos()
        {
            return _mediator.Send(new GetCargosQuery());
        }

        [HttpGet]
        [Route("getCiudades")]
        public Task<List<CiudadResp>> getCiudades()
        {
            return _mediator.Send(new GetCiudadesQuery());
        }
    }
}
