using InfoFretamento.Application.Request.PecasRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PecaController(PecaService service, EstoqueService estoqueService) : ControllerBase
    {
        private readonly EstoqueService _estoqueService = estoqueService;
        private readonly PecaService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrarPecaRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarPecaRequest request)
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

        [HttpPost("/retirada")]
        public async Task<IActionResult> RetirarPeca(RetirarPecaRequest request)
        {
            var response = await _estoqueService.RetirarPeca(request);
            return Ok(response);
        }

        [HttpGet("/retirada")]
        public async Task<IActionResult> GetAllRetiradas([FromQuery] DateTime? minDate= null, [FromQuery] DateTime? maxDate = null, [FromQuery] string? prefixoVeiculo = null)
        {
            if (minDate  == null)
            {
                minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            // Define o último dia do mês caso endDate seja nulo
            if (maxDate == null)
            {
                maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }


            var response = await _estoqueService.GetAllRetiradas(DateOnly.FromDateTime(minDate.Value), DateOnly.FromDateTime(maxDate.Value), prefixoVeiculo);

            return Ok(response);    
        }


        [HttpGet("/reestoque")]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? minDate = null, [FromQuery] DateTime? maxDate = null)
        {
            if (minDate == null)
            {
                minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            // Define o último dia do mês caso endDate seja nulo
            if (maxDate == null)
            {
                maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }


            var response = await _estoqueService.GetAllAdicionamentos(DateOnly.FromDateTime(minDate.Value), DateOnly.FromDateTime(maxDate.Value));

            return Ok(response);
        }


        [HttpPost("/reestoque")]
        public async Task<IActionResult> AdicionarPeca(AdicionarPecaRequest request)
        {
            var response = await _estoqueService.AdicionarPeca(request);
            return Ok(response);
        }

        [HttpDelete("/retirada")]
        public async Task<IActionResult> RemoverRetirada([FromRoute] int id)
        {
            var response = await _estoqueService.RemoverRetirada(id);
            return Ok(response);
        }

        [HttpDelete("/reestoque")]
        public async Task<IActionResult> RemoverAdicionamento([FromRoute] int id)
        {
            var response = await _estoqueService.RemoverRetirada(id);
            return Ok(response);
        }
    }
}
