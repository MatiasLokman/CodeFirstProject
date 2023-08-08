using API.Dtos.EmpleadosDtos.ConId;
using API.Dtos.EmpleadosDtos.SinId;
using API.Dtos.SucursalesDtos;
using API.Services.Commands.CreateEmpleadoCommand;
using API.Services.Commands.DeleteEmpleadoCommand;
using API.Services.Commands.UpdateEmpleadoCommand;
using API.Services.Queries.GetEmpleado;
using API.Services.Queries.GetEmpleadoById;
using API.Services.Queries.GetEmpleados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpleadoController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("/Empleados")]
        public async Task<ListaEmpleadosSinIdDto> GetEmpleados()
        {
            var empleados = await _mediator.Send(new GetEmpleadosQuery());
            return empleados;
        }


        [HttpGet("/Empleado")]
        public async Task<EmpleadoDto> GetEmpleado()
        {
            var empleado = await _mediator.Send(new GetEmpleadoQuery());
            return empleado;
        }


        [HttpGet("{id}")]
        public Task<EmpleadoDto> GetEmpleadoById(Guid id)
        {
            var empleado = _mediator.Send(new GetEmpleadoByIdQuery(id));
            return empleado;
        }


        [HttpPost]
        public async Task<EmpleadoSinIdDto> CreateEmpleado(CreateEmpleadoCommand command)
        {
            var empleadoCreado = await _mediator.Send(command);
            return empleadoCreado;
        }


        [HttpPut]
        public async Task<EmpleadoSinIdDto> UpdateEmpleado(UpdateEmpleadoCommand command)
        {
            var empleadoActualizado = await _mediator.Send(command);
            return empleadoActualizado;
        }


        [HttpDelete("{id}")]
        public async Task<EmpleadoSinIdDto> DeleteEmpleado(Guid id)
        {
            var empleadoEliminado = await _mediator.Send(new DeleteEmpleadoCommand { Id = id });
            return empleadoEliminado;
        }


    }
}
