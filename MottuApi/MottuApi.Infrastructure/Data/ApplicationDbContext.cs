using Microsoft.EntityFrameworkCore;
using MottuApi.Domain.Entities;

namespace MottuApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Filial>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefone).IsRequired().HasMaxLength(15);
                entity.OwnsOne(e => e.Endereco, endereco =>
                {
                    endereco.Property(e => e.Logradouro).IsRequired().HasMaxLength(100);
                    endereco.Property(e => e.Numero).IsRequired().HasMaxLength(10);
                    endereco.Property(e => e.Complemento).HasMaxLength(50);
                    endereco.Property(e => e.Bairro).IsRequired().HasMaxLength(50);
                    endereco.Property(e => e.Cidade).IsRequired().HasMaxLength(50);
                    endereco.Property(e => e.Estado).IsRequired().HasMaxLength(2);
                    endereco.Property(e => e.CEP).IsRequired().HasMaxLength(8);
                });
            });

            modelBuilder.Entity<Moto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Placa).IsRequired().HasMaxLength(7);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Cor).IsRequired().HasMaxLength(30);
                entity.HasOne(e => e.Filial)
                    .WithMany(f => f.Motos)
                    .HasForeignKey(e => e.FilialId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Telefone).IsRequired().HasMaxLength(15);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Cpf).IsUnique();
                entity.HasOne(e => e.Filial)
                    .WithMany()
                    .HasForeignKey(e => e.FilialId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
} 