using API.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Commands.CreateEmpleadoCommand
{
    public class CreateEmpleadoCommandValidator : AbstractValidator<CreateEmpleadoCommand>
    {
        private readonly CodeContext _context;

        public CreateEmpleadoCommandValidator(CodeContext context)
        {

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
                .MustAsync(ExisteEmpleado).WithMessage("El empleado ya existe");

            RuleFor(rule => rule)
                .MustAsync(ExisteCargo).WithMessage("No existe el cargo");

            RuleFor(rule => rule)
                .MustAsync(ExisteSucursal).WithMessage("No existe la sucursal");

            RuleFor(rule => rule)
                .MustAsync(ExisteJefe).WithMessage("No existe el jefe que trata de asignarle al empleado")
                .When(rule => rule.JefeId != Guid.Empty);

            _context = context;
        }

        private async Task<bool> ExisteEmpleado(CreateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Empleados.AnyAsync(p => p.Dni == command.Dni);

            return !existe;
        }

        private async Task<bool> ExisteCargo(CreateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Cargos.AnyAsync(p => p.Id == command.CargoId);
            return existe;
        }

        private async Task<bool> ExisteSucursal(CreateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Sucursales.AnyAsync(p => p.Id == command.SucursalId);
            return existe;
        }

        private async Task<bool> ExisteJefe(CreateEmpleadoCommand command, CancellationToken Token)
        {
            bool existe = await _context.Empleados.AnyAsync(p => p.JefeId == command.JefeId);
            return existe;
        }

    }
}
