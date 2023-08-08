using API.Dtos.EmpleadosDtos.SinId;
using MediatR;

namespace API.Services.Commands.CreateEmpleadoCommand
{
    public class CreateEmpleadoCommand : IRequest<EmpleadoSinIdDto>
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public Guid CargoId { get; set; }
        public Guid SucursalId { get; set; }
        public string? Dni { get; set; }
        public Guid? JefeId { get; set; }
    }
}
