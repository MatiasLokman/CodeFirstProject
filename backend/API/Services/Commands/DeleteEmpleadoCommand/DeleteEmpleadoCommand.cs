using API.Dtos.EmpleadosDtos.SinId;
using MediatR;
using System.Text.Json.Serialization;

namespace API.Services.Commands.DeleteEmpleadoCommand
{
    public class DeleteEmpleadoCommand : IRequest<EmpleadoSinIdDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
