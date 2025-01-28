using InfoFretamento.Application.Request.ManutencaoRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ManutencaoController(ManutencaoService service) : ControllerBase
    {
        private readonly ManutencaoService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? situacao = null, [FromQuery]DateTime? startDate = null, [FromQuery] string? veiculo = null)
        {
            if (startDate == null) {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            var result = await _service.GetAllWithFilters(DateOnly.FromDateTime(startDate.Value), situacao, veiculo);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarManutencaoRequest request)
        {
            var result = await _service.AddManutencaoAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarManutencaoRequest request)
        {
            request.Id = id;
            var result = await _service.UpdateAsync(request);
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
