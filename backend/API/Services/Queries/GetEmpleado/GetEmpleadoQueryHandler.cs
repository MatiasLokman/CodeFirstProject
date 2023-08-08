using AutoMapper;
using API.Data;
using API.Dtos.EmpleadosDtos.ConId;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Queries.GetEmpleado
{
    public class GetEmpleadoQueryHandler : IRequestHandler<GetEmpleadoQuery, EmpleadoDto>
    {
        private readonly CodeContext _context;
        private readonly IMapper _mapper;

        public GetEmpleadoQueryHandler(CodeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmpleadoDto> Handle(GetEmpleadoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var empleado = await _context.Empleados
                    .Include(x => x.Cargo)
                    .Include(x => x.Sucursal)
                    .ThenInclude(s => s.Ciudad)
                    .FirstAsync();

                if (empleado == null)
                {
                    var empleadoVacio = new EmpleadoDto();

                    empleadoVacio.StatusCode = StatusCodes.Status404NotFound;
                    empleadoVacio.IsSuccess = false;
                    empleadoVacio.ErrorMessage = "No se encontro un empleado con esas caracteristicas";

                    return empleadoVacio;
                }
                else
                {
                    var empleadoDTO = _mapper.Map<EmpleadoDto>(empleado);

                    empleadoDTO.StatusCode = StatusCodes.Status200OK;
                    empleadoDTO.IsSuccess = true;
                    empleadoDTO.ErrorMessage = "";

                    return empleadoDTO;
                }
            }
            catch (Exception ex)
            {
                var empleadoVacio = new EmpleadoDto();

                empleadoVacio.StatusCode = StatusCodes.Status400BadRequest;
                empleadoVacio.IsSuccess = false;
                empleadoVacio.ErrorMessage = ex.Message;

                return empleadoVacio;
            }
        }
    }
}
