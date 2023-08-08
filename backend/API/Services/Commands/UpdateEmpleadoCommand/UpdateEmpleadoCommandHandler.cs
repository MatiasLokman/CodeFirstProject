using AutoMapper;
using FluentValidation;
using API.Data;
using API.Dtos.EmpleadosDtos.SinId;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Commands.UpdateEmpleadoCommand
{
    public class UpdateEmpleadoCommandHandler : IRequestHandler<UpdateEmpleadoCommand, EmpleadoSinIdDto>
    {
        private readonly CodeContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateEmpleadoCommand> _validator;

        public UpdateEmpleadoCommandHandler(CodeContext context, IMapper mapper, IValidator<UpdateEmpleadoCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EmpleadoSinIdDto> Handle(UpdateEmpleadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var empleadoVacio = new EmpleadoSinIdDto();

                    empleadoVacio.StatusCode = StatusCodes.Status400BadRequest;
                    empleadoVacio.ErrorMessage = validationResult.ToString();
                    empleadoVacio.IsSuccess = false;

                    return empleadoVacio;
                }
                else
                {
                    var empleadoParaActualizar = await _context.Empleados.FindAsync(request.Id);

                    if (empleadoParaActualizar == null)
                    {
                        var empleadoVacio = new EmpleadoSinIdDto();

                        empleadoVacio.StatusCode = StatusCodes.Status404NotFound;
                        empleadoVacio.IsSuccess = false;
                        empleadoVacio.ErrorMessage = "No se encontro el empleado para actualizar";

                        return empleadoVacio;
                    }
                    else
                    {
                        empleadoParaActualizar.Nombre = request.Nombre;
                        empleadoParaActualizar.Apellido = request.Apellido;
                        empleadoParaActualizar.CargoId = request.CargoId;
                        empleadoParaActualizar.SucursalId = request.SucursalId;
                        empleadoParaActualizar.Dni = request.Dni;
                        empleadoParaActualizar.JefeId = request.JefeId;

                        await _context.SaveChangesAsync();


                        var empleadoEditado = await _context.Empleados
                        .Include(x => x.Cargo)
                        .Include(x => x.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                        .FirstOrDefaultAsync(x => x.Id == empleadoParaActualizar.Id);

                        var empleadoDTO = _mapper.Map<EmpleadoSinIdDto>(empleadoEditado);

                        empleadoDTO.StatusCode = StatusCodes.Status200OK;
                        empleadoDTO.IsSuccess = true;
                        empleadoDTO.ErrorMessage = "";

                        return empleadoDTO;
                    }
                }
            }
            catch (Exception ex)
            {
                var empleadoVacio = new EmpleadoSinIdDto();

                empleadoVacio.StatusCode = StatusCodes.Status400BadRequest;
                empleadoVacio.IsSuccess = false;
                empleadoVacio.ErrorMessage = ex.Message;

                return empleadoVacio;
            }
        }
    }
}
