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
    public class PostSucursal
    {
        public class PostSucursalCommand : IRequest<IResponseDTO>
        {
            public string Nombre { get; set; }
            public Guid CiudadID { get; set; }
        }

        public class PostSucursalCommandValidator : AbstractValidator<PostSucursalCommand>
        {
            private readonly ApplicationContext _context;
            public PostSucursalCommandValidator(ApplicationContext context)
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.CiudadID).NotEmpty();
                //RuleFor(x => x).MustAsync(CiudadExiste).WithMessage("La ciudad no existe");
                _context = context;
            }
        }

        public class PostSucursalCommandHandler : IRequestHandler<PostSucursalCommand, IResponseDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly PostSucursalCommandValidator _validator;

            public PostSucursalCommandHandler(ApplicationContext context, IMapper mapper, PostSucursalCommandValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<IResponseDTO> Handle(PostSucursalCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);
                try
                {
                    var sucursal = _mapper.Map<Sucursal>(request);

                    await _context.Sucursales.AddAsync(sucursal);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<SucursalResp>(sucursal);

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
