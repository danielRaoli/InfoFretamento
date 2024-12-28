using InfoFretamento.Application.Request.ClienteRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class ClienteController(ClienteService service) : ControllerBase
    {
        private readonly ClienteService _service = service;


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetWithIncludes(id);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllNameContains([FromQuery] string name = null)
        {
            var result = await _service.GetAllNameContains(name);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarClienteRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarClienteRequest request)
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
