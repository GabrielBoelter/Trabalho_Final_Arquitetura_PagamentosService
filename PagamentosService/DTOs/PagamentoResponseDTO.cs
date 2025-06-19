using PagamentosService.Models;

namespace PagamentosService.Models.DTOs
{
    public class PagamentoResponseDto
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TreinoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public StatusPagamento Status { get; set; }
        public string? TransacaoId { get; set; }
        public string? Observacoes { get; set; }
        public string NomeAluno { get; set; } = string.Empty;
        public string EmailAluno { get; set; } = string.Empty;
        public string NomeTreino { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}