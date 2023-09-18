using AutoMapper;
using DB;
using Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Applicationct.CQRS.Queries
{
    public class GetCargos
    {
        public class GetCargosQuery : IRequest<List<CargoResp>> { }
        public class GetCargosQueryHandler : IRequestHandler<GetCargosQuery, List<CargoResp>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public GetCargosQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CargoResp>> Handle(GetCargosQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cargo = await _context.Cargos.ToListAsync();
                    return _mapper.Map<List<CargoResp>>(cargo);
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, false);
                } 
            }
        }
    }
}
