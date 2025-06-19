namespace PagamentosService.Models
{
    public class AcessoTreino
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TreinoId { get; set; }
        public int PagamentoId { get; set; }
        public DateTime DataLiberacao { get; set; } = DateTime.Now;
        public DateTime DataExpiracao { get; set; }
        public bool Ativo { get; set; } = true;

        // Relacionamento
        public Pagamento Pagamento { get; set; } = null!;
    }
}