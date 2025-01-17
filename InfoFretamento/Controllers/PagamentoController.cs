using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PagamentoController(PagamentoService service) : ControllerBase
    {
        private readonly PagamentoService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddPagamento(PagamentoRequest request)
        {
            var response = await _service.AddAsync(request);    
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _service.RemoveAsync(id);
            return Ok(response);
        }
    }
}
