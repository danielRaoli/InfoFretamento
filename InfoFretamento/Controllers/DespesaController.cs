using InfoFretamento.Application.Request.PagamentoDespesaRequest;
using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DespesaController(DespesaService service) : ControllerBase
    {
        private readonly DespesaService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? mes = null, [FromQuery]int? ano =null, [FromQuery] int? despesaCode = null,[FromQuery] string status = "todas")
        {
            var dataAtual = DateTime.UtcNow.AddHours(-3);
            if(mes == null)
            {
                mes = dataAtual.Month;
            }
            if(ano == null)
            {
                ano = dataAtual.Year;
            }
            var result = await _service.GetAllWithFilterAsync(mes.Value, ano.Value, despesaCode,status);
            return Ok(result);
        }

        [HttpGet("{id}/{name}")]
        public async Task<IActionResult> GetByEntity([FromRoute]int id, [FromRoute] string name )
        {
          
            var result = await _service.GetByEntityId(id, name);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarDespesaRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }

        [HttpPost("pagamentodespesa")]
        public async Task<IActionResult> AdicionarPagamento([FromBody] AdicionarPagamentoDespesa request)
        {
            var result = await _service.AdicionarPagamento(request);
            return Ok(result);
        }

        [HttpPost("pagamentoboleto/{id}")]
        public async Task<IActionResult> PagarBoleto([FromRoute]int id)
        {
            var result = await _service.PagarBoleto(id);
            return Ok(result);
        }


        [HttpDelete("pagamentodespesa/{id}")]
        public async Task<IActionResult> AdicionarPagamento([FromRoute] int id)
        {
            var result = await _service.RemoverPagamento(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarDespesaRequest request)
        {
            request.Id = id;
            var result = await _service.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _service.RemoveAsync(id);
            return Ok(result);
        }
    }
}
