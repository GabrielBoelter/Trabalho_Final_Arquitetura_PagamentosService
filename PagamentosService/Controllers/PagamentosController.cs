using Microsoft.AspNetCore.Mvc;
using PagamentosService.Services;
using PagamentosService.DTOs;
using PagamentosService.Models.DTOs;

namespace PagamentosService.Controllers
{
    [ApiController]
    [Route("api/pagamentos")]
    public class PagamentosController : ControllerBase
    {
        private readonly IPagamentoService _service;

        public PagamentosController(IPagamentoService service)
        {
            _service = service;
        }

        // POST - Registrar pagamento
        [HttpPost]
        public IActionResult Post([FromBody] PagamentoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pagamento = _service.CreatePagamentoAsync(dto);
            return CreatedAtAction(nameof(Post), new { id = pagamento.Id }, pagamento);
        }

        // GET - Obter todos os pagamentos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAllPagamentosAsync());
        }
    }
}