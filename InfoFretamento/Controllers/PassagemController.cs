using InfoFretamento.Application.Request.PassagemRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
   
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PassagemController(PassagemService service) : ControllerBase
    {
        private readonly PassagemService _service = service;

        [HttpGet("viagem/{viagemId}")]
        public async Task<IActionResult> GetAll([FromRoute] int viagemId)
        {
            var result = await _service.GetAll(viagemId);
            return Ok(result);
        }
          

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarPassagemRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarPassagemRequest request)
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
