namespace API.Models
{
    public class Ciudad
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public virtual ICollection<Sucursal>? Empleados { get; set; }
    }
}
