namespace PagamentosService.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TreinoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; } = DateTime.Now;
        public DateTime DataVencimento { get; set; }
        public StatusPagamento Status { get; set; } = StatusPagamento.Pendente;
        public FormaPagamento FormaPagamento { get; set; }
        public string? Observacoes { get; set; }
        public string? TransacaoId { get; set; } // ID da transação externa

        // Dados do aluno (cache)
        public string NomeAluno { get; set; } = string.Empty;
        public string EmailAluno { get; set; } = string.Empty;

        // Dados do treino (cache)
        public string NomeTreino { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }
    }

    public enum StatusPagamento
    {
        Pendente = 1,
        Aprovado = 2,
        Rejeitado = 3,
        Cancelado = 4,
        Estornado = 5
    }

    public enum FormaPagamento
    {
        CartaoCredito = 1,
        CartaoDebito = 2,
        Pix = 3,
        Boleto = 4,
        Dinheiro = 5
    }
}