using PagamentosService.Models;

namespace PagamentosService.Repositories
{
    public interface IAcessoTreinoRepository
    {
        Task<IEnumerable<AcessoTreino>> GetAllAsync();
        Task<AcessoTreino?> GetByIdAsync(int id);
        Task<AcessoTreino> AddAsync(AcessoTreino acessoTreino);  // Método Adicionado
        Task<AcessoTreino?> UpdateAsync(int id, AcessoTreino acesso);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AcessoTreino>> GetByAlunoIdAsync(int alunoId);
        Task<AcessoTreino?> GetByAlunoAndTreinoAsync(int alunoId, int treinoId);
        Task<bool> TemAcessoAsync(int alunoId, int treinoId);
    }
}
