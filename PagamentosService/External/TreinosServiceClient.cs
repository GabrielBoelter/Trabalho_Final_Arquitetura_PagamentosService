using PagamentosService.External;

namespace PagamentosService.External
{
    public class TreinosServiceClient : ITreinosServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TreinosServiceClient> _logger;

        public TreinosServiceClient(HttpClient httpClient, ILogger<TreinosServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<TreinoExternalDto?> GetTreinoAsync(int treinoId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/treinos/{treinoId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<TreinoExternalDto>(content);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar treino {treinoId}");
                return null;
            }
        }

        public async Task<bool> TreinoExisteAsync(int treinoId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/treinos/{treinoId}/existe");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao verificar se treino {treinoId} existe");
                return false;
            }
        }
    }
}