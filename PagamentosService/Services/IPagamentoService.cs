using PagamentosService.DTOs;
using PagamentosService.Models;
using PagamentosService.Models.DTOs;

namespace PagamentosService.Services
{
    public interface IPagamentoService
    {
        Task<IEnumerable<PagamentoResponseDto>> GetAllPagamentosAsync();
        Task<PagamentoResponseDto?> GetPagamentoByIdAsync(int id);
        Task<PagamentoResponseDto?> CreatePagamentoAsync(PagamentoCreateDto pagamentoDto);
        Task<PagamentoResponseDto?> UpdatePagamentoAsync(int id, PagamentoUpdateDto pagamentoDto);
        Task<bool> DeletePagamentoAsync(int id);
        Task<IEnumerable<PagamentoResponseDto>> GetPagamentosByAlunoAsync(int alunoId);
        Task<IEnumerable<PagamentoResponseDto>> GetPagamentosByTreinoAsync(int treinoId);
        Task<IEnumerable<PagamentoResponseDto>> GetPagamentosByStatusAsync(StatusPagamento status);
        Task<bool> ProcessarPagamentoAsync(int pagamentoId);
        Task<bool> VerificarAcessoTreinoAsync(int alunoId, int treinoId);
    }
}
