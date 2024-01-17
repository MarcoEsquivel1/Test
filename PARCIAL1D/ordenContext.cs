using Microsoft.EntityFrameworkCore;
using PARCIAL1D.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D
{
    public class ordenContext : DbContext
    {

        public ordenContext(DbContextOptions<ordenContext> options) : base(options)
        {

        }

        public DbSet<DetalleOrdenes> DetalleOrdenes { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<EncabezadoOrden> EncabezadoOrden { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<Platos> Platos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
