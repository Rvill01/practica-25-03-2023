using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace practica_25_03_2023.Models
{
    public class universidadContext : DbContext
    {
        public universidadContext(DbContextOptions<universidadContext>options):base(options) 
        {

        }
        public DbSet<equipos> equipos { get; set; }
        public DbSet<carreras> carreras { get; set; }
        public DbSet<estados_equipo> estados_equipo { get; set; }
        public DbSet<estados_reserva> estados_reserva { get; set; }
        public DbSet<facultades> facultades { get; set; }
        public DbSet<marcas> marcas { get; set; }
        public DbSet<reservas> reservas { get; set; }
        public DbSet<tipo_equipo> tipos_equipo { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
    }
}
