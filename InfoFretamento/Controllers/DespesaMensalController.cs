using InfoFretamento.Application.Request.GastosMensais;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DespesaMensalController(SalarioService service, DespesaMensalService despesaService) : ControllerBase
    {
        private readonly SalarioService _sarioService = service;
        private readonly DespesaMensalService _despesaMensalService = despesaService;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _sarioService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddSalario(AdicionarSalarioRequest request)
        {
            var response = await _sarioService.AddAsync(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalario(AtualizarSalarioRequest request)
        {
            var response = await _sarioService.UpdateAsync(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var response = await _sarioService.RemoveAsync(id);
            return Ok(response);
        }


        [HttpGet("despesamensal")]
        public async Task<IActionResult> AllDespesa()
        {
            var response = await _despesaMensalService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost("despesamensal")]
        public async Task<IActionResult> AddDespesa(AdicionarDespesaMensal request)
        {
            var response = await _despesaMensalService.AddAsync(request);
            return Ok(response);
        }

        [HttpPut("despesamensal")]
        public async Task<IActionResult> UpdateSalario(AtualizarDespesaMensal request)
        {
            var response = await _despesaMensalService.UpdateAsync(request);
            return Ok(response);
        }

        [HttpDelete("despesamensal/{id}")]
        public async Task<IActionResult> RemoveDespesa([FromRoute] int id)
        {
            var response = await _despesaMensalService.RemoveAsync(id);
            return Ok(response);
        }
    }
}
