using AutoMapper;
using DB;
using DB.Models;
using Application.Dtos;
using FluentValidation;
using MediatR;
using Application.Dtos.Inteface;
using System.Net;

namespace Application.CQRS.Commands.Post
{
    public class PostEmpleado
    {
        public class PostEmpleadoCommand : IRequest<IResponseDTO>
        {
            public string Nombre { get; set; }

            public string Apellido { get; set; }

            public Guid SucursalID { get; set; }

            public string Dni { get; set; }

            public Guid CargoID { get; set; }

            public Guid JefeId { get; set; }
        }

        public class PostEmpleadoCommandValidation : AbstractValidator<PostEmpleadoCommand>
        {
            public PostEmpleadoCommandValidation()
            {
                RuleFor(e => e.Nombre).NotEmpty();
                RuleFor(e => e.Apellido).NotEmpty();
                RuleFor(e => e.SucursalID).NotEmpty().NotNull();
                RuleFor(e => e.Dni).NotEmpty();
                RuleFor(e => e.CargoID).NotEmpty();
                RuleFor(e => e.JefeId).NotEmpty();
            }
        }

        public class PostEmpleadoCommandHandler : IRequestHandler<PostEmpleadoCommand, IResponseDTO>
        {
            private readonly ApplicationContext _Context;
            private readonly PostEmpleadoCommandValidation _Validation;
            private readonly IMapper _Mapper;

            public PostEmpleadoCommandHandler(ApplicationContext context, PostEmpleadoCommandValidation validation, IMapper mapper)
            {
                _Context = context;
                _Validation = validation;
                _Mapper = mapper;
            }

            public async Task<IResponseDTO> Handle(PostEmpleadoCommand request, CancellationToken cancellationToken)
            {
                _Validation.Validate(request);
                try
                {
                    var emp = _Mapper.Map<Empleado>(request);
                    emp.FechaAlta = DateTime.Now.ToUniversalTime();

                    await _Context.Empleados.AddAsync(emp);
                    await _Context.SaveChangesAsync();

                    return _Mapper.Map<EmpleadoResp>(emp);
                }
                catch (Exception ex)
                {
                    RespBase res = new RespBase();
                    res.SetErrorMsj(ex.Message);
                    res.Status = HttpStatusCode.NotFound;
                    return res;
                }
            }
        }
    }
}
