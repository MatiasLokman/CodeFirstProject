namespace API.Dtos.SucursalesDtos
{
    public class ListaSucursalesDto : RespuestaBase
    {
        public List<SucursalDto>? Sucursales { get; set; }
    }
}
