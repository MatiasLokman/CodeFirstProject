using AutoMapper;
using FluentValidation;
using API.Data;
using API.Dtos.EmpleadosDtos.ConId;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Queries.GetEmpleadoById
{
    public class GetEmpleadoByIdQueryHandler : IRequestHandler<GetEmpleadoByIdQuery, EmpleadoDto>
    {
        private readonly CodeContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<GetEmpleadoByIdQuery> _validator;

        public GetEmpleadoByIdQueryHandler(CodeContext context, IMapper mapper, IValidator<GetEmpleadoByIdQuery> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EmpleadoDto> Handle(GetEmpleadoByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var EmpleadoVacio = new EmpleadoDto();

                    EmpleadoVacio.StatusCode = StatusCodes.Status400BadRequest;
                    EmpleadoVacio.ErrorMessage = string.Join(". ", validationResult.Errors.Select(e => e.ErrorMessage));
                    EmpleadoVacio.IsSuccess = false;

                    return EmpleadoVacio;
                }
                else
                {
                    var empleado = await _context.Empleados
                        .Include(x => x.Cargo)
                        .Include(x => x.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                        .FirstAsync(p => p.Id == request.id);

                    if (empleado == null)
                    {
                        var ProductoVacio = new EmpleadoDto();

                        ProductoVacio.StatusCode = StatusCodes.Status404NotFound;
                        ProductoVacio.ErrorMessage = "El empleado no existe";
                        ProductoVacio.IsSuccess = false;

                        return ProductoVacio;
                    }
                    else
                    {
                        var empleadoDto = _mapper.Map<EmpleadoDto>(empleado);

                        empleadoDto.StatusCode = StatusCodes.Status200OK;
                        empleadoDto.IsSuccess = true;
                        empleadoDto.ErrorMessage = "";

                        return empleadoDto;
                    }
                }
            }
            catch (Exception ex)
            {
                var EmpleadoVacio = new EmpleadoDto();

                EmpleadoVacio.StatusCode = StatusCodes.Status400BadRequest;
                EmpleadoVacio.ErrorMessage = ex.Message;
                EmpleadoVacio.IsSuccess = false;

                return EmpleadoVacio;
            }
        }
    }
}
