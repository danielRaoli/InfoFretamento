using InfoFretamento.Application.Request.FeriasRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeriasController(FeriasService service) : ControllerBase
    {
        private readonly FeriasService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? year = null)
        {

            if (year == null) { 
            
                year = DateTime.Now.Year;
            }

            var result = await _service.GetAllWithFilterAsync(year.Value);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarFeriasRequest request)
        {
            var result = await _service.AddAsync(request);
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
