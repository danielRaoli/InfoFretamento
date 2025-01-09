using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DashboardController(DashBoardService service) : ControllerBase
    {
        private readonly DashBoardService _service = service;

        [HttpGet("/viagens")]
        public async Task<IActionResult> GetViagens()
        {
            var response = await _service.TotalViagens();
            return Ok(response);
        }

        [HttpGet("/despesas")]
        public async Task<IActionResult> GetDespesas()
        {
            var response = await _service.TotalDespesas();
            return Ok(response);
        }

        [HttpGet("/receita")]
        public async Task<IActionResult> GetReceitas()
        {
            var response = await _service.TotalReceitas();
            return Ok(response);
        }

        [HttpGet("/liquido")]
        public async Task<IActionResult> GetLiquido([FromQuery] int? ano = null)
        {
            if(ano == null)
            {
                ano = DateTime.Now.Year;
            }

            var response = await _service.ValorLiquidoMensal(ano.Value);
            return Ok(response);
        }
    }
}
