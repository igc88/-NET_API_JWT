using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_API_SQL_Proveedores.Models {
    public class APIContext : DbContext {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }
        public DbSet<Pieza> Cientificos { get; set; }
        public DbSet<Proveedor> Proyectos { get; set; }
        public DbSet<Suministro> Asignaciones { get; set; }

        public virtual DbSet<UserInfo> UserInfo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Pieza>(entity => {
                entity.ToTable("Piezas");

                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Proveedor>(entity => {
                entity.ToTable("Proveedores");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("Id")
                    .HasColumnType("char")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Suministro>(entity => {
                entity.ToTable("Suministra");

                entity.HasKey(e => new { e.CodigoPieza, e.ProveedorId });

                entity.Property(e => e.ProveedorId).HasColumnName("IdProveedor");

                entity.HasOne(d => d.Pieza).WithMany(p => p.Suministros).HasForeignKey(d => d.CodigoPieza).HasForeignKey("FK1");
                entity.HasOne(d => d.Proveedor).WithMany(p => p.Suministros).HasForeignKey(d => d.ProveedorId).HasForeignKey("FK2");

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
