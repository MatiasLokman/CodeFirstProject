namespace API.Dtos.EmpleadosDtos.SinId
{
    public class EmpleadoSinIdDto : RespuestaBase
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? CargoNombre { get; set; }
        public string? SucursalNombre { get; set; }
        public string? CiudadNombre { get; set; }
        public string? Dni { get; set; }
        public DateTime FechaAlta { get; set; }
        public string? JefeNombre { get; set; }
    }
}
