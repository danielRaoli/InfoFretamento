using InfoFretamento.Application.Request.AbastecimentoRequests;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class AbastecimentoController(AbastecimentoService service) : ControllerBase
    {

        private readonly AbastecimentoService _service = service;


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarAbastecimentoRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarAbastecimentoRequest request)
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
