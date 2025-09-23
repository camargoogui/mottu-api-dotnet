using Microsoft.EntityFrameworkCore;
using MottuApi.Domain.Entities; // Ajuste o namespace conforme seu projeto

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
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ CONFIGURAÇÃO ESSENCIAL - Cascade Delete
            modelBuilder.Entity<Moto>(entity =>
            {
                // Configurar relacionamento Moto -> Filial
                entity.HasOne(m => m.Filial)
                      .WithMany(f => f.Motos)
                      .HasForeignKey(m => m.FilialId)
                      .OnDelete(DeleteBehavior.Cascade) // ← ESTA LINHA RESOLVE O PROBLEMA!
                      .IsRequired();

                // Índice único para placa
                entity.HasIndex(m => m.Placa)
                      .IsUnique();

                // Configurações de propriedades
                entity.Property(m => m.Placa)
                      .HasMaxLength(7)
                      .IsRequired();

                entity.Property(m => m.Modelo)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(m => m.Cor)
                      .HasMaxLength(30)
                      .IsRequired();
            });

            modelBuilder.Entity<Filial>(entity =>
            {
                entity.Property(f => f.Nome)
                      .HasMaxLength(100)
                      .IsRequired();

                // Configurar Endereco como um Owned Type
                entity.OwnsOne(f => f.Endereco, endereco =>
                {
                    endereco.Property(e => e.Logradouro)
                           .HasMaxLength(100)
                           .IsRequired();
                    endereco.Property(e => e.Numero)
                           .HasMaxLength(10)
                           .IsRequired();
                    endereco.Property(e => e.Complemento)
                           .HasMaxLength(50);
                    endereco.Property(e => e.Bairro)
                           .HasMaxLength(50)
                           .IsRequired();
                    endereco.Property(e => e.Cidade)
                           .HasMaxLength(50)
                           .IsRequired();
                    endereco.Property(e => e.Estado)
                           .HasMaxLength(2)
                           .IsRequired();
                    endereco.Property(e => e.CEP)
                           .HasMaxLength(8)
                           .IsRequired();
                });
            });

            modelBuilder.Entity<Locacao>(entity =>
            {
                // Configurar relacionamentos
                entity.HasOne(l => l.Moto)
                      .WithMany()
                      .HasForeignKey(l => l.MotoId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();

                // Configurações de propriedades
                entity.Property(l => l.DataInicio)
                      .IsRequired();

                entity.Property(l => l.ClienteNome)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(l => l.ClienteCpf)
                      .HasMaxLength(11)
                      .IsRequired();

                entity.Property(l => l.ClienteTelefone)
                      .HasMaxLength(15)
                      .IsRequired();

                // Configurar campos decimais com precisão adequada
                entity.Property(l => l.ValorHora)
                      .HasPrecision(10, 2)
                      .IsRequired();

                entity.Property(l => l.ValorTotal)
                      .HasPrecision(10, 2);
            });
        }
    }
}