using AutoMapper;
using DB;
using Application.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Inteface;
using System.Net;

namespace Application.CQRS.Commands.Put
{
    public class UpdateCredencial
    {
        public class UpdateCredencialCommand : IRequest<IResponseDTO>
        {            
            public string Usuario { get; set; }
            public string Contrasenia { get; set; }            
        }
        public class PostCiudadCommandValidator : AbstractValidator<UpdateCredencialCommand>
        {
            public PostCiudadCommandValidator()
            {
                RuleFor(x => x.Usuario).NotEmpty().NotNull();
                RuleFor(x => x.Contrasenia).NotEmpty().NotNull();            
            }
        }
        public class PostCiudadCommandHandler : IRequestHandler<UpdateCredencialCommand, IResponseDTO>
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

            public async Task<IResponseDTO> Handle(UpdateCredencialCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);
                try
                {
                    var credencial = await _context.Credenciales.FirstOrDefaultAsync(c =>
                        c.Usuario.Equals(request.Usuario)
                    );

                    if (credencial != null)
                    {
                        credencial.Contrasenia = request.Contrasenia;
                        credencial.ActualidoEn = DateTime.Now.ToUniversalTime();
                        
                        await _context.SaveChangesAsync();

                        return _mapper.Map<CredencialResp>(credencial);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    RespBase res = new RespBase();
                    res.SetErrorMsj("El Usuario no coincide. -" + ex.Message);
                    res.Status = HttpStatusCode.NotFound;
                    return res;
                }
            }
        }
    }
}
