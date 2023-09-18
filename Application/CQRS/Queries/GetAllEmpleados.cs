using AutoMapper;
using DB;
using Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Inteface;
using Application.Exceptions;

namespace Application.CQRS.Queries
{
    public class GetAllEmpleados
    {
        public class GetAllEmpleadosQuery : IRequest<List<EmpleadoResp>>
        {

        }

        public class GetAllEmpleadosQueryHandler : IRequestHandler<GetAllEmpleadosQuery, List<EmpleadoResp>>
        {
            private readonly ApplicationContext _Context;
            private readonly IMapper _Mapper;

            public GetAllEmpleadosQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _Context = context;
                _Mapper = mapper;
            }

            public async Task<List<EmpleadoResp>> Handle(GetAllEmpleadosQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var Empleados = await _Context.Empleados
                        .Include(x => x.Cargo)
                        .Include(x => x.Sucursal)
                        .Include(x => x.Sucursal.Ciudad)
                        .Where(x => x.Activo.Equals(0))
                        .ToListAsync();

                    return _Mapper.Map<List<EmpleadoResp>>(Empleados);
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, false);
                }
            }
        }
    }
}
