using ExamenOneCore.Entity.Models;

using ExamenOneCore.Entity.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ExamenOneCore.Data.Contexts
{
    public partial class ExamenOneCoreBDContext : DbContext
    {
        private string _dbConnection = string.Empty;

        public ExamenOneCoreBDContext(string dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ExamenOneCoreBDContext(DbContextOptions<ExamenOneCoreBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_dbConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}