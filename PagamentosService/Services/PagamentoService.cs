using PagamentosService.DTOs;
using PagamentosService.External;
using PagamentosService.Models;
using PagamentosService.Models.DTOs;
using PagamentosService.Repositories;
using PagamentosService.Services;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IAcessoTreinoRepository _acessoRepository;
    private readonly ITreinosServiceClient _treinosClient;
    private readonly ILogger<PagamentoService> _logger;

    public PagamentoService(
        IPagamentoRepository pagamentoRepository,
        IAcessoTreinoRepository acessoRepository,
        ITreinosServiceClient treinosClient,
        ILogger<PagamentoService> logger)
    {
        _pagamentoRepository = pagamentoRepository;
        _acessoRepository = acessoRepository;
        _treinosClient = treinosClient;
        _logger = logger;
    }

    public async Task<IEnumerable<PagamentoResponseDto>> GetAllPagamentosAsync()
    {
        var pagamentos = await _pagamentoRepository.GetAllAsync();
        var result = new List<PagamentoResponseDto>();
        foreach (var pagamento in pagamentos)
        {
            var dto = MapToResponseDto(pagamento); // Removido await
            result.Add(dto);
        }
        return result;
    }

    public async Task<PagamentoResponseDto?> GetPagamentoByIdAsync(int id)
    {
        var pagamento = await _pagamentoRepository.GetByIdAsync(id);
        return pagamento != null ? MapToResponseDto(pagamento) : null; // Removido await
    }

    public async Task<PagamentoResponseDto?> CreatePagamentoAsync(PagamentoCreateDto pagamentoDto)
    {
        var treinoExiste = await _treinosClient.TreinoExisteAsync(pagamentoDto.TreinoId);
        if (!treinoExiste)
        {
            _logger.LogWarning($"Tentativa de criar pagamento para treino inexistente: {pagamentoDto.TreinoId}");
            return null;
        }

        var pagamento = new Pagamento
        {
            AlunoId = pagamentoDto.AlunoId,
            TreinoId = pagamentoDto.TreinoId,
            Valor = pagamentoDto.Valor,
            DataVencimento = pagamentoDto.DataVencimento,
            FormaPagamento = pagamentoDto.FormaPagamento,
            Observacoes = pagamentoDto.Observacoes,
            NomeAluno = pagamentoDto.NomeAluno,
            EmailAluno = pagamentoDto.EmailAluno,
            NomeTreino = pagamentoDto.NomeTreino,
            Status = StatusPagamento.Pendente,
            DataCriacao = DateTime.Now
        };

        var createdPagamento = await _pagamentoRepository.CreateAsync(pagamento);
        return MapToResponseDto(createdPagamento); 
    }

    public async Task<PagamentoResponseDto?> UpdatePagamentoAsync(int id, PagamentoUpdateDto pagamentoDto)
    {
        var existingPagamento = await _pagamentoRepository.GetByIdAsync(id);
        if (existingPagamento == null) return null;

        if (pagamentoDto.Status.HasValue) existingPagamento.Status = pagamentoDto.Status.Value;
        if (pagamentoDto.TransacaoId != null) existingPagamento.TransacaoId = pagamentoDto.TransacaoId;
        if (pagamentoDto.Observacoes != null) existingPagamento.Observacoes = pagamentoDto.Observacoes;

        var updatedPagamento = await _pagamentoRepository.UpdateAsync(id, existingPagamento);

        if (pagamentoDto.Status == StatusPagamento.Aprovado)
        {
            await ProcessarPagamentoAsync(id);
        }

        return updatedPagamento != null ? MapToResponseDto(updatedPagamento) : null; 
    }

    public async Task<bool> DeletePagamentoAsync(int id)
    {
        return await _pagamentoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PagamentoResponseDto>> GetPagamentosByAlunoAsync(int alunoId)
    {
        var pagamentos = await _pagamentoRepository.GetByAlunoIdAsync(alunoId);
        var result = new List<PagamentoResponseDto>();
        foreach (var pagamento in pagamentos)
        {
            var dto = MapToResponseDto(pagamento); // Removido await
            result.Add(dto);
        }
        return result;
    }

    public async Task<IEnumerable<PagamentoResponseDto>> GetPagamentosByTreinoAsync(int treinoId)
    {
        var pagamentos = await _pagamentoRepository.GetByTreinoIdAsync(treinoId);
        var result = new List<PagamentoResponseDto>();
        foreach (var pagamento in pagamentos)
        {
            var dto = MapToResponseDto(pagamento); // Removido await
            result.Add(dto);
        }
        return result;
    }

    public async Task<IEnumerable<PagamentoResponseDto>> GetPagamentosByStatusAsync(StatusPagamento status)
    {
        var pagamentos = await _pagamentoRepository.GetByStatusAsync(status);
        var result = new List<PagamentoResponseDto>();
        foreach (var pagamento in pagamentos)
        {
            var dto = MapToResponseDto(pagamento); // Removido await
            result.Add(dto);
        }
        return result;
    }

    public async Task<bool> ProcessarPagamentoAsync(int pagamentoId)
    {
        var pagamento = await _pagamentoRepository.GetByIdAsync(pagamentoId);
        if (pagamento == null || pagamento.Status != StatusPagamento.Aprovado)
            return false;

        var acessoExistente = await _acessoRepository.GetByAlunoAndTreinoAsync(pagamento.AlunoId, pagamento.TreinoId);
        if (acessoExistente != null)
            return true;

        var acesso = new AcessoTreino
        {
            AlunoId = pagamento.AlunoId,
            TreinoId = pagamento.TreinoId,
            PagamentoId = pagamento.Id,
            DataLiberacao = DateTime.Now,
            DataExpiracao = DateTime.Now.AddMonths(1),
            Ativo = true
        };

        await _acessoRepository.AddAsync(acesso);
        return true;
    }

    public async Task<bool> VerificarAcessoTreinoAsync(int alunoId, int treinoId)
    {
        var acesso = await _acessoRepository.GetByAlunoAndTreinoAsync(alunoId, treinoId);
        return acesso != null && acesso.Ativo && acesso.DataExpiracao > DateTime.Now;
    }

    private PagamentoResponseDto MapToResponseDto(Pagamento pagamento)
    {
        return new PagamentoResponseDto
        {
            Id = pagamento.Id,
            AlunoId = pagamento.AlunoId,
            TreinoId = pagamento.TreinoId,
            Valor = pagamento.Valor,
            DataVencimento = pagamento.DataVencimento,
            FormaPagamento = pagamento.FormaPagamento,
            Status = pagamento.Status,
            TransacaoId = pagamento.TransacaoId,
            Observacoes = pagamento.Observacoes,
            NomeAluno = pagamento.NomeAluno,
            EmailAluno = pagamento.EmailAluno,
            NomeTreino = pagamento.NomeTreino,
            DataCriacao = pagamento.DataCriacao
        };
    }
}