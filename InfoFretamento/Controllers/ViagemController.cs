using InfoFretamento.Application.Request.ViagemRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ViagemController(ViagemService service) : ControllerBase
    {
        private readonly ViagemService _service = service;


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? startDate= null, [FromQuery] DateTime? endDate = null, [FromQuery] string? prefixoVeiculo = null)
        {
            if (startDate == null)
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.UtcNow.AddHours(-3).Month, 1);
            }

            // Define o último dia do mês caso endDate seja nulo
            if (endDate == null)
            {
                endDate = new DateTime(DateTime.Now.Year, 12, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.UtcNow.AddHours(-3).Month));
            }
            var result = await _service.GetAllWithFilters(startDate: DateOnly.FromDateTime(startDate.Value), endDate: DateOnly.FromDateTime(endDate.Value), prefixoVeiculo: prefixoVeiculo);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetWithFilter(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarViagemRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarViagemRequest request)
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
