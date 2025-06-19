using PagamentosService.Models;
using System.ComponentModel.DataAnnotations;

namespace PagamentosService.DTOs
{
    public class PagamentoRequestDTO
    {
        [Required(ErrorMessage = "AlunoId é obrigatório")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "TreinoId é obrigatório")]
        public int TreinoId { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatória")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Forma de pagamento é obrigatória")]
        public FormaPagamento FormaPagamento { get; set; }

        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "Nome do aluno é obrigatório")]
        public string NomeAluno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email do aluno é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string EmailAluno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome do treino é obrigatório")]
        public string NomeTreino { get; set; } = string.Empty;
    }
}