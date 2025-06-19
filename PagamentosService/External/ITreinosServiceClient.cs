namespace PagamentosService.External
{
    public interface ITreinosServiceClient
    {
        Task<bool> TreinoExisteAsync(int treinoId);
        Task<TreinoExternalDto?> GetTreinoAsync(int treinoId);
    }
}