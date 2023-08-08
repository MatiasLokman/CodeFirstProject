using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public partial class CodeContext : DbContext
    {
        public CodeContext(DbContextOptions<CodeContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
    }
}
