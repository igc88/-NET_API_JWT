using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_API_SQL_Reservas.Models {
    public class APIContext : DbContext {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }
        public DbSet<Investigador> Investigadores { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Facultad> Facultades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Investigador>(entity => {
                entity.ToTable("Investigadores");

                entity.HasKey(e => e.DNI);

                entity.HasOne(d => d.Facultad).WithMany(p => p.Investigadores).HasForeignKey(d => d.IdFacultad).HasForeignKey("FK1");
            });

            modelBuilder.Entity<Equipo>(entity => {
                entity.ToTable("Equipos");

                entity.HasKey(e => e.NumSerie);

                entity.HasOne(d => d.Facultad).WithMany(p => p.Equipos).HasForeignKey(d => d.IdFacultad).HasForeignKey("FK1");
            });

            modelBuilder.Entity<Facultad>(entity => {
                entity.ToTable("Facultades");

                entity.HasKey(e => e.Codigo);
            });

            modelBuilder.Entity<Reserva>(entity => {
                entity.ToTable("Reservas");

                entity.HasKey(e => new { e.InvestigadorDNI, e.EquipoNumReferencia });

                entity.Property(e => e.EquipoNumReferencia).HasColumnName("NumSerie");
                entity.Property(e => e.InvestigadorDNI).HasColumnName("DNI");

                entity.HasOne(d => d.Investigador).WithMany(p => p.Reservas).HasForeignKey(d => d.InvestigadorDNI).HasForeignKey("FK1");
                entity.HasOne(d => d.Equipo).WithMany(p => p.Reservas).HasForeignKey(d => d.EquipoNumReferencia).HasForeignKey("FK2");

            });

            modelBuilder.Entity<UserInfo>(entity => {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}
