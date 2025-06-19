using PagamentosService.Models;

namespace PagamentosService.DTOs
{
    public class PagamentoUpdateDto
    {
        public StatusPagamento? Status { get; set; }
        public string? TransacaoId { get; set; }
        public string? Observacoes { get; set; }
    }
}