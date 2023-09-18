using AutoMapper;
using DB;
using Application.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Inteface;
using System.Net;

namespace Application.CQRS.Commands.Post
{
    public class LogCredencial
    {
        public class LogCredencialCommand : IRequest<IResponseDTO>
        {
            public string Usuario { get; set; }
            public string Contrasenia { get; set; }

            public LogCredencialCommand(string usuario, string contrasenia)
            {
                Usuario = usuario;
                Contrasenia = contrasenia;
            }
        }

        public class LogCredencialQueryValidation : AbstractValidator<LogCredencialCommand>
        {
            public LogCredencialQueryValidation()
            {
                RuleFor(c => c.Usuario).NotEmpty().NotNull().MaximumLength(12).MinimumLength(3);
                RuleFor(c => c.Contrasenia).NotEmpty().NotNull().MaximumLength(14).MinimumLength(5);
            }
        }

        public class LogCredencialQueryHandler : IRequestHandler<LogCredencialCommand, IResponseDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly LogCredencialQueryValidation _validation;

            public LogCredencialQueryHandler(ApplicationContext context, IMapper mapper, LogCredencialQueryValidation validation)
            {
                _context = context;
                _mapper = mapper;
                _validation = validation;
            }

            public async Task<IResponseDTO> Handle(LogCredencialCommand request, CancellationToken cancellationToken)
            {
                _validation.Validate(request);
                try
                {
                    var credencial = await _context.Credenciales.FirstOrDefaultAsync(c =>
                        c.Usuario.Equals(request.Usuario) && c.Contrasenia.Equals(request.Contrasenia)
                    );

                    if (credencial != null)
                    {
                        return _mapper.Map<CredencialResp>(credencial);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    var res = new RespBase();
                    res.SetErrorMsj("El usuario y/o contraseña son incorrectos. - " + ex.Message);
                    res.Status = HttpStatusCode.BadRequest;
                    return res;
                }

            }
        }
    }
}
