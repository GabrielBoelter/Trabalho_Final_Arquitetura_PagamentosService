using PagamentosService.Models;

namespace PagamentosService.Repositories
{
    public interface IPagamentoRepository
    {
        Task<IEnumerable<Pagamento>> GetAllAsync();
        Task<Pagamento?> GetByIdAsync(int id);
        Task<Pagamento> CreateAsync(Pagamento pagamento);
        Task<Pagamento?> UpdateAsync(int id, Pagamento pagamento);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Pagamento>> GetByAlunoIdAsync(int alunoId);
        Task<IEnumerable<Pagamento>> GetByTreinoIdAsync(int treinoId);
        Task<IEnumerable<Pagamento>> GetByStatusAsync(StatusPagamento status);
        Task<Pagamento?> GetByAlunoAndTreinoAsync(int alunoId, int treinoId);
    }
}