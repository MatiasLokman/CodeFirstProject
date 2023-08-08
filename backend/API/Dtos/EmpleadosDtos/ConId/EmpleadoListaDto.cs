using API.Dtos.SucursalesDtos;
using API.Models;

namespace API.Dtos.EmpleadosDtos.ConId
{
    public class EmpleadoListaDto
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public CargoDto Cargo { get; set; }
        public SucursalDto Sucursal { get; set; }
        public string? Dni { get; set; }
        public DateTime FechaAlta { get; set; }
        public EmpleadoDto? Jefe { get; set; }
    }
}
