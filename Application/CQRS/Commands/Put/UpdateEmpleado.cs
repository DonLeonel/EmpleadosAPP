using AutoMapper;
using DB;
using DB.Models;
using Application.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Inteface;
using System.Net;

namespace Application.CQRS.Commands.Put
{
    public class UpdateEmpleado
    {
        public class UpdateEmpleadoCommand : IRequest<IResponseDTO>
        {
            public Guid EmpleadoID { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public Guid SucursalID { get; set; }
            public string Dni { get; set; }
            public Guid CargoID { get; set; }
            public Guid JefeId { get; set; }
        }

        public class PostEmpleadoCommandValidation : AbstractValidator<UpdateEmpleadoCommand>
        {
            public PostEmpleadoCommandValidation()
            {
                RuleFor(e => e.EmpleadoID).NotEmpty().NotNull();
                RuleFor(e => e.Nombre).NotEmpty();
                RuleFor(e => e.Apellido).NotEmpty();
                RuleFor(e => e.SucursalID).NotEmpty().NotNull();
                RuleFor(e => e.Dni).NotEmpty();
                RuleFor(e => e.CargoID).NotEmpty();
                RuleFor(e => e.JefeId).NotEmpty();
            }
        }

        public class PostEmpleadoCommandHandler : IRequestHandler<UpdateEmpleadoCommand, IResponseDTO>
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

            public async Task<IResponseDTO> Handle(UpdateEmpleadoCommand request, CancellationToken cancellationToken)
            {
                _Validation.Validate(request);
                try
                {
                    Empleado? empleado = await _Context.Empleados.FirstOrDefaultAsync(e => 
                        e.EmpleadoID.Equals(request.EmpleadoID)
                    );
                    
                    if(empleado != null) 
                    {
                        empleado.Nombre = request.Nombre;
                        empleado.Apellido = request.Apellido;
                        empleado.Dni = request.Dni;
                        empleado.SucursalID = request.SucursalID;
                        empleado.CargoID = request.CargoID;
                        empleado.JefeID = request.JefeId;

                        await _Context.SaveChangesAsync();

                        return _Mapper.Map<EmpleadoResp>(empleado);
                    }
                    else
                    {
                        throw new Exception();
                    }                                   
                }
                catch (Exception ex)
                {
                    RespBase res = new RespBase();
                    res.SetErrorMsj("El Empleado no coincide. -" + ex.Message);
                    res.Status = HttpStatusCode.NotFound;
                    return res;
                }
            }
        }
    }
}
