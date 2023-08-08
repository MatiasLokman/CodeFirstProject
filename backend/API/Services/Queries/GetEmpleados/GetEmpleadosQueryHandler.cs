using API.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using API.Dtos.EmpleadosDtos.SinId;

namespace API.Services.Queries.GetEmpleados
{
    public class GetEmpleadosQueryHandler : IRequestHandler<GetEmpleadosQuery, ListaEmpleadosSinIdDto>
    {
        private readonly CodeContext _context;
        private readonly IMapper _mapper;

        public GetEmpleadosQueryHandler(CodeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaEmpleadosSinIdDto> Handle(GetEmpleadosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var empleados = await _context.Empleados
                    .Include(x => x.Cargo)
                    .Include(x => x.Sucursal)
                    .ThenInclude(s => s.Ciudad)
                    .ToListAsync();

                if (empleados.Count > 0)
                {
                    var empleadosDTO = _mapper.Map<List<EmpleadoListaSinIdDto>>(empleados);

                    var listaEmpleadosDto = new ListaEmpleadosSinIdDto
                    {
                        Empleados = empleadosDTO
                    };

                    listaEmpleadosDto.StatusCode = StatusCodes.Status200OK;
                    listaEmpleadosDto.ErrorMessage = string.Empty;
                    listaEmpleadosDto.IsSuccess = true;

                    return listaEmpleadosDto;
                }
                else
                {
                    var listaEmpleadosVacia = new ListaEmpleadosSinIdDto();

                    listaEmpleadosVacia.StatusCode = StatusCodes.Status404NotFound;
                    listaEmpleadosVacia.ErrorMessage = "No se han encontrado empleados";
                    listaEmpleadosVacia.IsSuccess = false;

                    return listaEmpleadosVacia;
                }

            }
            catch (Exception ex)
            {
                var listaEmpleadosVacia = new ListaEmpleadosSinIdDto();

                listaEmpleadosVacia.StatusCode = StatusCodes.Status400BadRequest;
                listaEmpleadosVacia.ErrorMessage = ex.Message;
                listaEmpleadosVacia.IsSuccess = false;

                return listaEmpleadosVacia;
            }
        }
    }
}
