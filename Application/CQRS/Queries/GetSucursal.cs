using AutoMapper;
using DB;
using Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Application.Dtos.Inteface;
using Application.Exceptions;

namespace Application.CQRS.Queries
{
    public class GetSucursal
    {
        public class GetSucursalQuery : IRequest<List<SucursalResp>>
        {
        }
        public class GetSucursalQueryHandler : IRequestHandler<GetSucursalQuery, List<SucursalResp>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetSucursalQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<SucursalResp>> Handle(GetSucursalQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var sucursal = await _context.Sucursales
                        .Include(x => x.Ciudad)
                        .ToListAsync();

                    return _mapper.Map<List<SucursalResp>>(sucursal);                     
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, false);
                }
            }
        }
    }
}
