using AutoMapper;
using FluentValidation;
using API.Data;
using API.Dtos.EmpleadosDtos.SinId;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Commands.DeleteEmpleadoCommand
{
    public class DeleteEmpleadoCommandHandler : IRequestHandler<DeleteEmpleadoCommand, EmpleadoSinIdDto>
    {

        private readonly CodeContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<DeleteEmpleadoCommand> _validator;

        public DeleteEmpleadoCommandHandler(CodeContext context, IMapper mapper, IValidator<DeleteEmpleadoCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EmpleadoSinIdDto> Handle(DeleteEmpleadoCommand request, CancellationToken cancellationToken)
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
                    var empleadoParaEliminar = await _context.Empleados.FindAsync(request.Id);

                    if (empleadoParaEliminar == null)
                    {
                        var empleadoVacio = new EmpleadoSinIdDto();

                        empleadoVacio.StatusCode = StatusCodes.Status404NotFound;
                        empleadoVacio.IsSuccess = false;
                        empleadoVacio.ErrorMessage = "No se encontro el empleado para eliminar";

                        return empleadoVacio;
                    }
                    else
                    {
                        var empleadoEliminado = await _context.Empleados
                        .Include(x => x.Cargo)
                        .Include(x => x.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                        .FirstOrDefaultAsync(x => x.Id == empleadoParaEliminar.Id);


                        _context.Empleados.Remove(empleadoEliminado);
                        await _context.SaveChangesAsync();

                        var empleadoDTO = _mapper.Map<EmpleadoSinIdDto>(empleadoEliminado);

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
