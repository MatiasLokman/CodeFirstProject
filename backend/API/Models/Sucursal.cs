using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Sucursal
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }


        [ForeignKey("Ciudad")]
        public Guid CiudadId { get; set; }
        public virtual Ciudad? Ciudad { get; set; }


        public virtual ICollection<Empleado>? Empleados { get; set; }
    }
}
