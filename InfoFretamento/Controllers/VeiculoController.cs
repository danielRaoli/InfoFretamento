﻿using InfoFretamento.Application.Request.VeiculoRequest;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoFretamento.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class VeiculoController(VeiculoService service) : ControllerBase
    {
        private readonly VeiculoService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetWithInclude(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarVeiculoRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarVeiculoRequest request)
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
