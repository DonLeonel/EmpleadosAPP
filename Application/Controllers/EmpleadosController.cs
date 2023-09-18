using Application.Dtos;
using Application.Dtos.Inteface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Commands.Delete.DeleteEmpleado;
using static Application.CQRS.Commands.Post.PostEmpleado;
using static Application.CQRS.Commands.Put.UpdateEmpleado;
using static Application.CQRS.Queries.GetAllEmpleados;

namespace ExamFinalProgII_Refact
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public EmpleadosController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost]
        [Route("saveEmpleado")] 
        public Task<IResponseDTO> saveEmpleado([FromBody] PostEmpleadoCommand postEmpleadoCommand)
        {
            return _Mediator.Send(postEmpleadoCommand);
        }

        [HttpGet]
        [Route("getAll")]
        public Task<List<EmpleadoResp>> getAllEmpleados()
        {
            return _Mediator.Send(new GetAllEmpleadosQuery());
        }

        [HttpPut]
        [Route("put")]
        public Task<IResponseDTO> putEmpleado([FromBody] UpdateEmpleadoCommand updateEmpleadoCommand)
        {
            return _Mediator.Send(updateEmpleadoCommand);
        }

        [HttpDelete("delete/{uuid}")]
        public Task<bool> putEmpleado(Guid uuid)
        {
            return _Mediator.Send(new DeleteEmpleadoCommand() { EmpleadoID = uuid});
        }
    }
}
