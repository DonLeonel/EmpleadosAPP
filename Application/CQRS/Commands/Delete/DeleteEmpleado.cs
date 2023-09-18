using Application.Exceptions;
using DB;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Commands.Delete
{
    public class DeleteEmpleado
    { 
        public class DeleteEmpleadoCommand : IRequest<bool>
        {
            public Guid EmpleadoID { get; set; }
        }

        public class DeleteEmpleadoCommandValidation : AbstractValidator<DeleteEmpleadoCommand>
        {
            public DeleteEmpleadoCommandValidation()
            {
                RuleFor(e => e.EmpleadoID).NotEmpty().NotNull();
            }
        }

        public class DeleteEmpleadoCommandHandler : IRequestHandler<DeleteEmpleadoCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly DeleteEmpleadoCommandValidation _validation;

            public DeleteEmpleadoCommandHandler(ApplicationContext context, DeleteEmpleadoCommandValidation validation)
            {
                _context = context;
                _validation = validation;
            }

            public async Task<bool> Handle(DeleteEmpleadoCommand request, CancellationToken cancellationToken)
            {
                _validation.Validate(request);
                try
                {
                    var empleado = await _context.Empleados.FirstOrDefaultAsync(emp =>
                        emp.EmpleadoID.Equals(request.EmpleadoID)
                    );

                    if (empleado != null)
                    {
                        empleado.Activo = 1;  //por default: (0 = activo) & (1 = !Activo)
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;                        
                    }
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message, false);
                }

            }
        }
    }
}
