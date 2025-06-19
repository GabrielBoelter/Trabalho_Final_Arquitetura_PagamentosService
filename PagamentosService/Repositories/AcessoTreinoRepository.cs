using Microsoft.EntityFrameworkCore;
using PagamentosService.Data;
using PagamentosService.Models;

namespace PagamentosService.Repositories
{
    public class AcessoTreinoRepository : IAcessoTreinoRepository
    {
        private readonly AppDbContext _context;

        public AcessoTreinoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para obter todos os acessos
        public async Task<IEnumerable<AcessoTreino>> GetAllAsync()
        {
            return await _context.AcessosTreino
                .Include(a => a.Pagamento)
                .ToListAsync();
        }

        // Método para obter um acesso por ID
        public async Task<AcessoTreino?> GetByIdAsync(int id)
        {
            return await _context.AcessosTreino
                .Include(a => a.Pagamento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Método para adicionar um novo acesso
        public async Task<AcessoTreino> AddAsync(AcessoTreino acessoTreino)
        {
            await _context.AcessosTreino.AddAsync(acessoTreino);
            await _context.SaveChangesAsync();
            return acessoTreino;
        }

        // Método para atualizar um acesso existente
        public async Task<AcessoTreino?> UpdateAsync(int id, AcessoTreino acesso)
        {
            var existingAcesso = await _context.AcessosTreino.FindAsync(id);
            if (existingAcesso == null) return null;

            existingAcesso.DataExpiracao = acesso.DataExpiracao;
            existingAcesso.Ativo = acesso.Ativo;

            await _context.SaveChangesAsync();
            return existingAcesso;
        }

        // Método para deletar um acesso
        public async Task<bool> DeleteAsync(int id)
        {
            var acesso = await _context.AcessosTreino.FindAsync(id);
            if (acesso == null) return false;

            _context.AcessosTreino.Remove(acesso);
            await _context.SaveChangesAsync();
            return true;
        }

        // Método para obter acessos por aluno
        public async Task<IEnumerable<AcessoTreino>> GetByAlunoIdAsync(int alunoId)
        {
            return await _context.AcessosTreino
                .Include(a => a.Pagamento)
                .Where(a => a.AlunoId == alunoId && a.Ativo)
                .OrderByDescending(a => a.DataLiberacao)
                .ToListAsync();
        }

        // Método para obter acesso por aluno e treino
        public async Task<AcessoTreino?> GetByAlunoAndTreinoAsync(int alunoId, int treinoId)
        {
            return await _context.AcessosTreino
                .Include(a => a.Pagamento)
                .Where(a => a.AlunoId == alunoId && a.TreinoId == treinoId && a.Ativo)
                .FirstOrDefaultAsync();
        }

        // Método para verificar se o aluno tem acesso ao treino
        public async Task<bool> TemAcessoAsync(int alunoId, int treinoId)
        {
            return await _context.AcessosTreino
                .AnyAsync(a => a.AlunoId == alunoId &&
                              a.TreinoId == treinoId &&
                              a.Ativo &&
                              a.DataExpiracao > DateTime.Now);
        }
    }
}
