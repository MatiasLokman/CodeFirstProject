using API.Dtos.EmpleadosDtos.SinId;
using MediatR;

namespace API.Services.Commands.UpdateEmpleadoCommand
{
    public class UpdateEmpleadoCommand : IRequest<EmpleadoSinIdDto>
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public Guid CargoId { get; set; }
        public Guid SucursalId { get; set; }
        public string? Dni { get; set; }
        public Guid? JefeId { get; set; }
    }
}
