using vendinhaPlena.Models;
using Microsoft.EntityFrameworkCore;

namespace vendinhaPlena.Data.Repositories
{
    public class VendaDbContext : DbContext
    {
        public DbSet<Cliente> Cliente => Set<Cliente>();
        public DbSet<Dividas> Divida => Set<Dividas>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                Environment.GetEnvironmentVariable(
                    "ConnectionStrings__DefaultConnection"
                    )
            );

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var modelCliente = modelBuilder.Entity<Cliente>();
            var modelDivida = modelBuilder.Entity<Dividas>();

            modelCliente
                .HasMany(c => c.dividas)
                .WithOne(d => d.Cliente)
                .HasForeignKey(d => d.idCliente);

            modelCliente.ToTable("cliente");

            modelCliente.Property(e => e.id).HasColumnName("id");
            modelCliente.Property(e => e.nome).HasColumnName("nome");
            modelCliente.Property(e => e.cpf).HasColumnName("cpf");
            modelCliente.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            modelCliente.Property(e => e.Email).HasColumnName("email");
            modelCliente.HasKey(e => e.id);

            modelDivida.ToTable("dividas");
            modelDivida.Property(e => e.id_divida).HasColumnName("id_divida");
            modelDivida.Property(e => e.valor).HasColumnName("valor");
            modelDivida.Property(e => e.situacao).HasColumnName("situacao");
            modelDivida.Property(e => e.dataCriacao).HasColumnName("data_criacao");
            modelDivida.Property(e => e.dataPagamento).HasColumnName("data_pagamento");
            modelDivida.Property(e => e.idCliente).HasColumnName("id_cliente");
            modelDivida.HasKey(e => e.id_divida);

            base.OnModelCreating(modelBuilder);
        }
    }
}