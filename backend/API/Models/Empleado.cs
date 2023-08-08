using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Empleado
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }


        [ForeignKey("Cargo")]
        public Guid CargoId { get; set; }
        public virtual Cargo? Cargo { get; set; }


        [ForeignKey("Sucursal")]
        public Guid SucursalId { get; set; }
        public virtual Sucursal? Sucursal { get; set; }


        public string? Dni { get; set; }
        public DateTime FechaAlta { get; set; }


        [ForeignKey("Jefe")]
        public Guid? JefeId { get; set; }
        public virtual Empleado? Jefe { get; set; }


        public virtual ICollection<Empleado>? Empleados { get; set; }
    }
}
