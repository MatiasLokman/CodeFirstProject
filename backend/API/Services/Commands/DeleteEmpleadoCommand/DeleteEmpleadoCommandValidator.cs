using FluentValidation;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Commands.DeleteEmpleadoCommand
{
    public class DeleteEmpleadoCommandValidator : AbstractValidator<DeleteEmpleadoCommand>
    {
        private readonly CodeContext _context;

        public DeleteEmpleadoCommandValidator(CodeContext context)
        {
            RuleFor(rule => rule.Id)
                .NotEmpty().WithMessage("Id del empleado no puede estar vacio");

            RuleFor(rule => rule)
                .MustAsync(ExisteEmpleado).WithMessage("No se encontro ningún empleado con ese Id");

            RuleFor(rule => rule)
               .MustAsync(ExisteRelacionJefe).WithMessage("No se puede eliminar este empleado ya que es jefe de otro empleado");

            _context = context;
        }

        private async Task<bool> ExisteEmpleado(DeleteEmpleadoCommand command, CancellationToken Token)
        {
            bool ExisteEmpleado = await _context.Empleados.AnyAsync(p => p.Id == command.Id);
            return ExisteEmpleado;
        }

        private async Task<bool> ExisteRelacionJefe(DeleteEmpleadoCommand command, CancellationToken Token)
        {
            bool tieneRelacionConOtrosEmpleados = await _context.Empleados.AnyAsync(e => e.JefeId == command.Id);

            if (tieneRelacionConOtrosEmpleados)
            {
                return false;
            }

            return true;
        }

    }
}
