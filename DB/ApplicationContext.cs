using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Credencial> Credenciales { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }

    }
}