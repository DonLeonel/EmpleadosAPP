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
    public class PostCiudad
    {
        public class PostCiudadCommand : IRequest<IResponseDTO>
        {
            public string Nombre { get; set; } 
        }
        public class PostCiudadCommandValidator : AbstractValidator<PostCiudadCommand>
        {
            public PostCiudadCommandValidator()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }
        public class PostCiudadCommandHandler : IRequestHandler<PostCiudadCommand, IResponseDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly PostCiudadCommandValidator _validator;

            public PostCiudadCommandHandler(ApplicationContext context, IMapper mapper, PostCiudadCommandValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<IResponseDTO> Handle(PostCiudadCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);
                try
                {
                    var ciudad = _mapper.Map<Ciudad>(request);

                    await _context.Ciudades.AddAsync(ciudad);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<CiudadResp>(ciudad);

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
