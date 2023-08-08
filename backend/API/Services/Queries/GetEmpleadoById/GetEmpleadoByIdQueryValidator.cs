using FluentValidation;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Queries.GetEmpleadoById
{
    public class GetEmpleadoByIdQueryValidator : AbstractValidator<GetEmpleadoByIdQuery>
    {
        private readonly CodeContext _context;

        public GetEmpleadoByIdQueryValidator(CodeContext context)
        {
            _context = context;

            RuleFor(p => p.id)
          .NotEmpty().WithMessage("El id no puede estar vacío")
          .NotNull().WithMessage("El id no puede ser nulo")
          .MustAsync(ExisteEmpleado).WithMessage("El id: {PropertyValue} no existe, ingrese un id de un empleado existente");
        }

        private async Task<bool> ExisteEmpleado(Guid id, CancellationToken token)
        {
            bool existe = await _context.Empleados.AnyAsync(p => p.Id == id);
            return existe;
        }

    }
}
