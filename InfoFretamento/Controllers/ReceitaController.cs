using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReceitaController(ReceitaService service) : ControllerBase
    {
        private readonly ReceitaService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? mes = null, [FromQuery] int? ano = null, [FromQuery] string status = "todas", [FromQuery] int? receitaId = null)
        {
            var dataAtual = DateTime.UtcNow.AddHours(-3);
            if (mes == null)
            {
                mes = dataAtual.Month;
            }
            if (ano == null)
            {
                ano = dataAtual.Year;
            }

            var result = await _service.GetAllWithFilterAsync(mes.Value, ano.Value,status, receitaId);
            return Ok(result);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarReceitaRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarReceitaRequest request)
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
