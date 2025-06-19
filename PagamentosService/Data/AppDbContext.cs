using Microsoft.EntityFrameworkCore;
using PagamentosService.Models;

namespace PagamentosService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<AcessoTreino> AcessosTreino { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade Pagamento
            modelBuilder.Entity<Pagamento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.AlunoId).IsRequired();
                entity.Property(e => e.TreinoId).IsRequired();
                entity.Property(e => e.Valor).IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(e => e.NomeAluno).IsRequired().HasMaxLength(100);
                entity.Property(e => e.EmailAluno).IsRequired().HasMaxLength(150);
                entity.Property(e => e.NomeTreino).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DataPagamento).IsRequired();
                entity.Property(e => e.DataVencimento).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FormaPagamento).IsRequired();
                entity.Property(e => e.Observacoes).HasMaxLength(500);
                entity.Property(e => e.TransacaoId).HasMaxLength(100);
                entity.Property(e => e.DataCriacao).IsRequired();
            });

            // Configuração da entidade AcessoTreino
            modelBuilder.Entity<AcessoTreino>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.AlunoId).IsRequired();
                entity.Property(e => e.TreinoId).IsRequired();
                entity.Property(e => e.PagamentoId).IsRequired();
                entity.Property(e => e.DataLiberacao).IsRequired();
                entity.Property(e => e.DataExpiracao).IsRequired();
                entity.Property(e => e.Ativo).IsRequired();

                // Relacionamento
                entity.HasOne(e => e.Pagamento)
                      .WithMany()
                      .HasForeignKey(e => e.PagamentoId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Índice único para evitar duplicatas
                entity.HasIndex(e => new { e.AlunoId, e.TreinoId, e.PagamentoId })
                      .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}