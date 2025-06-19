using Microsoft.EntityFrameworkCore;
using PagamentosService.Data;
using PagamentosService.Models;

namespace PagamentosService.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly AppDbContext _context;

        public PagamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pagamento>> GetAllAsync()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        public async Task<Pagamento?> GetByIdAsync(int id)
        {
            return await _context.Pagamentos.FindAsync(id);
        }

        public async Task<Pagamento> CreateAsync(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
            return pagamento;
        }

        public async Task<Pagamento?> UpdateAsync(int id, Pagamento pagamento)
        {
            var existingPagamento = await _context.Pagamentos.FindAsync(id);
            if (existingPagamento == null) return null;

            existingPagamento.Status = pagamento.Status;
            existingPagamento.TransacaoId = pagamento.TransacaoId;
            existingPagamento.Observacoes = pagamento.Observacoes;
            existingPagamento.DataAtualizacao = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingPagamento;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null) return false;

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Pagamento>> GetByAlunoIdAsync(int alunoId)
        {
            return await _context.Pagamentos
                .Where(p => p.AlunoId == alunoId)
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pagamento>> GetByTreinoIdAsync(int treinoId)
        {
            return await _context.Pagamentos
                .Where(p => p.TreinoId == treinoId)
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pagamento>> GetByStatusAsync(StatusPagamento status)
        {
            return await _context.Pagamentos
                .Where(p => p.Status == status)
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();
        }

        public async Task<Pagamento?> GetByAlunoAndTreinoAsync(int alunoId, int treinoId)
        {
            return await _context.Pagamentos
                .Where(p => p.AlunoId == alunoId && p.TreinoId == treinoId)
                .OrderByDescending(p => p.DataCriacao)
                .FirstOrDefaultAsync();
        }
    }
}