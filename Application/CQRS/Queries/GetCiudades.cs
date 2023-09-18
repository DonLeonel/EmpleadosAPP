using AutoMapper;
using DB;
using Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.CQRS.Queries
{
    public class GetCiudades
    {
        public class GetCiudadesQuery : IRequest<List<CiudadResp>> { }
        public class GetCiudadesQueryHandler : IRequestHandler<GetCiudadesQuery, List<CiudadResp>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetCiudadesQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CiudadResp>> Handle(GetCiudadesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var ciudad = await _context.Ciudades.ToListAsync();
                    return _mapper.Map<List<CiudadResp>>(ciudad);
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, false);
                }
            }
        }
    }
}
