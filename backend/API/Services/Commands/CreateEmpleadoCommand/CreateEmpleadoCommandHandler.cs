using API.Data;
using API.Models;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using API.Dtos.EmpleadosDtos.SinId;

namespace API.Services.Commands.CreateEmpleadoCommand
{
    public class CreateEmpleadoCommandHandler : IRequestHandler<CreateEmpleadoCommand, EmpleadoSinIdDto>
    {
        private readonly CodeContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEmpleadoCommand> _validator;
        public CreateEmpleadoCommandHandler(CodeContext context, IMapper mapper, IValidator<CreateEmpleadoCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EmpleadoSinIdDto> Handle(CreateEmpleadoCommand request, CancellationToken cancellationToken)
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
                    var emlpleadoParaCrear = _mapper.Map<Empleado>(request);

                    emlpleadoParaCrear.Id = Guid.NewGuid();
                    emlpleadoParaCrear.FechaAlta = DateTime.Now.ToUniversalTime();

                    await _context.AddAsync(emlpleadoParaCrear);
                    await _context.SaveChangesAsync();


                    var empleadoEncontrado = await _context.Empleados
                    .Include(x => x.Cargo)
                    .Include(x => x.Sucursal)
                    .ThenInclude(s => s.Ciudad)
                    .FirstOrDefaultAsync(x => x.Id == emlpleadoParaCrear.Id);


                    var empleadoDTO = _mapper.Map<EmpleadoSinIdDto>(empleadoEncontrado);

                    empleadoDTO.StatusCode = StatusCodes.Status200OK;
                    empleadoDTO.IsSuccess = true;
                    empleadoDTO.ErrorMessage = "";

                    return empleadoDTO;
                }
            }
            catch (Exception ex)
            {
                var empleadoVacio = new EmpleadoSinIdDto();

                empleadoVacio.StatusCode = StatusCodes.Status400BadRequest;
                empleadoVacio.ErrorMessage = ex.Message;
                empleadoVacio.IsSuccess = false;

                if (ex.InnerException != null)
                {
                    empleadoVacio.ErrorMessage += " Inner Exception: " + ex.InnerException.Message;
                }

                return empleadoVacio;
            }
        }
    }
}
