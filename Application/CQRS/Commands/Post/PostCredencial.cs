using AutoMapper;
using DB.Models;
using DB;
using Application.Dtos;
using FluentValidation;
using MediatR;
using Application.Dtos.Inteface;
using System.Net;

namespace Application.CQRS.Commands.Post
{
    public class PostCredencial
    {
        public class PostCredencialCommand : IRequest<IResponseDTO>
        {
            public string Usuario { get; set; }
            public string Contrasenia { get; set; }
            public string Avatar { get; set; }
        }
        public class PostCiudadCommandValidator : AbstractValidator<PostCredencialCommand>
        {
            public PostCiudadCommandValidator()
            {
                RuleFor(x => x.Usuario).NotEmpty().NotNull();
                RuleFor(x => x.Contrasenia).NotEmpty().NotNull();
                RuleFor(x => x.Avatar).NotEmpty().NotNull();

            }
        }
        public class PostCiudadCommandHandler : IRequestHandler<PostCredencialCommand, IResponseDTO>
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

            public async Task<IResponseDTO> Handle(PostCredencialCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);
                try
                {
                    var credencial = _mapper.Map<Credencial>(request);

                    credencial.CreadoEn = DateTime.Now.ToUniversalTime();
                    credencial.ActualidoEn = DateTime.Now.ToUniversalTime();                    

                    await _context.Credenciales.AddAsync(credencial);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<CredencialResp>(credencial);
                }
                catch (Exception ex)
                {
                    var res = new RespBase();
                    res.SetErrorMsj(ex.Message);
                    res.Status = HttpStatusCode.NotFound;
                    return res;
                }
            }
        }
    }
}
