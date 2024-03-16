using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlmoxarifadoAPI.Models
{
    public partial class AlmoxarifadoAPIContext : DbContext
    {
        public AlmoxarifadoAPIContext()
        {
        }

        public AlmoxarifadoAPIContext(DbContextOptions<AlmoxarifadoAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GestaoProduto> GestaoProdutos { get; set; } = null!;
        public virtual DbSet<Logrobo> LOGROBO { get; set; } = null!;
        public virtual DbSet<Email> Emails { get; set; } = null!; // Adicionando DbSet para a classe Email

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data source=dbbenchmarking.cfk2cqm644xo.us-east-2.rds.amazonaws.com; Database=almoxarifadoequipe01; User Id=admin; Password=master12;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GestaoProduto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__GestaoPr__5EEDF7C3DE3C8957");

                entity.Property(e => e.IdProduto)
                    .ValueGeneratedNever()
                    .HasColumnName("idProduto");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.EstoqueAtual).HasColumnName("estoqueAtual");

                entity.Property(e => e.EstoqueMinimo).HasColumnName("estoqueMinimo");

                entity.Property(e => e.Preco)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("preco");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<Logrobo>(entity =>
            {
                entity.HasKey(e => e.IDlOg);

                entity.ToTable("LOGROBO");

                entity.Property(e => e.IDlOg).HasColumnName("iDlOG");

                entity.Property(e => e.IdProdutoAPI).HasColumnName("IdProdutoAPI");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.idEmail); 

                entity.ToTable("Emails");

                entity.Property(e => e.idEmail).HasColumnName("idEmail"); 

                entity.Property(e => e.EmailUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("EmailUsuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
