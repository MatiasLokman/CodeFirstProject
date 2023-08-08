namespace API.Models
{
    public class Cargo
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public virtual ICollection<Empleado>? Empleados { get; set; }
    }
}
