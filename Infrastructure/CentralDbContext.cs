using Business.Model.Users;
using Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public partial class CentralDbContext : DbContext
    {
        public CentralDbContext(DbContextOptions<CentralDbContext> options) : base(options)
        {
        }

        public virtual DbSet<UserDTO> Usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDTO>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.RememberToken).HasColumnName("remember_token");
                entity.Property(e => e.IsGestor).HasColumnName("is_gestor");
                entity.Property(e => e.DataUltimoLogin).HasColumnName("data_ultimo_login");
                entity.Property(e => e.DataUltimoLogout).HasColumnName("data_ultimo_logout");
                entity.Property(e => e.DataUltimaAtividade).HasColumnName("data_ultima_atividade");
                entity.Property(e => e.IdCli).HasColumnName("id_cli");
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp").HasColumnName("created_at")
                .HasConversion(
                v => v, 
                v => v == DateTime.MinValue ? DateTime.Now : v 
            ); ;
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp").HasColumnName("updated_at").HasConversion(
                v => v, 
                v => v == DateTime.MinValue ? DateTime.Now : v 
            ); ;
            });
        }
    }

}
