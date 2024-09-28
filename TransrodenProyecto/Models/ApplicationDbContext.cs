using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransrodenProyecto.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Calculadora> Calculadoras { get; set; }

        public DbSet<Camion> Camiones { get; set; }

        public DbSet<Carga> Cargas { get; set; }

        public DbSet<Envio> Envios { get; set; }

        public DbSet<Facturacion> Facturaciones { get; set; }

        public DbSet<Historial> Historiales { get; set; }

        public DbSet<Paquete> Paquetes { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuración para la relación Envio -> Usuario
            modelBuilder.Entity<Envio>()
                .HasRequired(e => e.Usuario)
                .WithMany(u => u.Envios)
                .HasForeignKey(e => e.Id_Usuario)
                .WillCascadeOnDelete(false);  // Evitar eliminación en cascada

            // Configuración para la relación Paquete -> Envio
            modelBuilder.Entity<Paquete>()
                .HasRequired(p => p.Envio)
                .WithMany(e => e.Paquetes)
                .HasForeignKey(p => p.Id_Envio)
                .WillCascadeOnDelete(false);  // Evitar eliminación en cascada

            // Configuración para la relación Carga -> Usuario
            modelBuilder.Entity<Carga>()
                .HasRequired(c => c.Usuario)
                .WithMany(u => u.Cargas)
                .HasForeignKey(c => c.Id_Usuario)
                .WillCascadeOnDelete(false);  // Evitar eliminación en cascada

            // Configuración para la relación Paquete -> Carga
            modelBuilder.Entity<Paquete>()
                .HasRequired(p => p.Carga)
                .WithMany(c => c.Paquetes)
                .HasForeignKey(p => p.Id_Carga)
                .WillCascadeOnDelete(false);  // Evitar eliminación en cascada

            base.OnModelCreating(modelBuilder);

        }

    }
}