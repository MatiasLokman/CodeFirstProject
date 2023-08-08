using API.Dtos.EmpleadosDtos.ConId;
using MediatR;

namespace API.Services.Queries.GetEmpleadoById
{
    public record GetEmpleadoByIdQuery(Guid id) : IRequest<EmpleadoDto>;
}
