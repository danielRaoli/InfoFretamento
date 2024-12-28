using InfoFretamento.Application.Request.ClienteRequest;
using InfoFretamento.Application.Request.ViagemProgramadaRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class ViagemProgramadaController(ViagemProgramadaService service) : ControllerBase
    {
        private readonly ViagemProgramadaService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdIncludeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarViagemProgramadaRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarViagemProgramadaRequest request)
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
