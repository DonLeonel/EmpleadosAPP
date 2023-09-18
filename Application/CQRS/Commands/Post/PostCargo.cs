using Application.Dtos;
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
    public class PostCargo
    {
        public class PostCargoCommand : IRequest<IResponseDTO>
        {
            public string Nombre { get; set; } 
        }
        public class PostCargoCommandValidator : AbstractValidator<PostCargoCommand>
        {
            public PostCargoCommandValidator()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }
        public class PostCargoCommandHandler : IRequestHandler<PostCargoCommand, IResponseDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly PostCargoCommandValidator _validator;

            public PostCargoCommandHandler(ApplicationContext context, IMapper mapper, PostCargoCommandValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<IResponseDTO> Handle(PostCargoCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);                
                try
                {
                    Cargo cargo = _mapper.Map<Cargo>(request);
                    await _context.Cargos.AddAsync(cargo);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<CargoResp>(cargo);

                }
                catch (Exception e)
                {
                    RespBase res = new RespBase();
                    res.SetErrorMsj(e.Message);
                    res.Status = HttpStatusCode.NotFound; 
                    return res;
                   
                }
            }
        }
    }
}
