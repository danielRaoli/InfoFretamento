using InfoFretamento.Application.Request.AbastecimentoRequests;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AbastecimentoController(AbastecimentoService service) : ControllerBase
    {

        private readonly AbastecimentoService _service = service;



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarAbastecimentoRequest request)
        {
            var result = await _service.Add(request);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.RemoveAsync(id);
            return Ok(result);
        }
    }
}
