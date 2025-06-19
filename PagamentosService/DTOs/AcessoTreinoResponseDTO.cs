namespace PagamentosService.DTOs
{
    public class AcessoTreinoResponseDTO
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TreinoId { get; set; }
        public int PagamentoId { get; set; }
        public DateTime DataLiberacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public bool Ativo { get; set; }
        public string NomeTreino { get; set; } = string.Empty;
        public string StatusPagamento { get; set; } = string.Empty;
    }
}
