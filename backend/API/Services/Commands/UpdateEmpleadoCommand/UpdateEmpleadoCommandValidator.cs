using FluentValidation;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Commands.UpdateEmpleadoCommand
{
    public class UpdateEmpleadoCommandValidator : AbstractValidator<UpdateEmpleadoCommand>
    {
        private readonly CodeContext _context;

        public UpdateEmpleadoCommandValidator(CodeContext context)
        {
            RuleFor(x => x.Id)
             .NotNull().WithMessage("{PropertyName} no puede ser nulo")
             .NotEmpty().WithMessage("{PropertyName} no puede estar vacio");

            RuleFor(x => x.Nombre)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo")
            .NotEmpty().WithMessage("{PropertyName} no puede estar vacio");

            RuleFor(x => x.Apellido)
             .NotNull().WithMessage("{PropertyName} no puede ser nulo")
             .NotEmpty().WithMessage("{PropertyName} no puede estar vacio");

            RuleFor(x => x.CargoId)
             .NotNull().WithMessage("{PropertyName} no puede ser nulo")
             .NotEmpty().WithMessage("{PropertyName} no puede estar vacio");

            RuleFor(x => x.SucursalId)
             .NotNull().WithMessage("{PropertyName} no puede ser nulo")
             .NotEmpty().WithMessage("{PropertyName} no puede estar vacio");

            RuleFor(x => x.Dni)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo")
            .NotEmpty().WithMessage("{PropertyName} no puede estar vacio");

            RuleFor(rule => rule)
                .MustAsync(ExisteEmpleado).WithMessage("El empleado no existe");

            RuleFor(rule => rule)
                .MustAsync(ExisteCargo).WithMessage("No existe el cargo");

            RuleFor(rule => rule)
                .MustAsync(ExisteSucursal).WithMessage("No existe la sucursal");

            RuleFor(rule => rule)
                .MustAsync(ExisteJefe).WithMessage("No existe el jefe que trata de asignarle al empleado")
                .When(rule => rule.JefeId != Guid.Empty && rule.JefeId != null);

            _context = context;
        }

        private async Task<bool> ExisteEmpleado(UpdateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Empleados.AnyAsync(p => p.Dni == command.Dni);

            return existe;
        }

        private async Task<bool> ExisteCargo(UpdateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Cargos.AnyAsync(p => p.Id == command.CargoId);
            return existe;
        }

        private async Task<bool> ExisteSucursal(UpdateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Sucursales.AnyAsync(p => p.Id == command.SucursalId);
            return existe;
        }

        private async Task<bool> ExisteJefe(UpdateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Empleados.AnyAsync(p => p.JefeId == command.JefeId);
            return existe;
        }

    }
}
