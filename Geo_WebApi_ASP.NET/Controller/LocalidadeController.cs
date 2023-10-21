using FluentValidation;
using Geo_WebApi_ASP.NET.Model;
using Geo_WebApi_ASP.NET.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Geo_WebApi_ASP.NET.Controller
{
    [Authorize]
    [Route("~/localidade")]
    [ApiController]
    public class LocalidadeController : ControllerBase
    {
        private readonly ILocalidadeService _localidadeService;
        private readonly IValidator<Localidade> _localidadeValidator;


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
        return Ok(await _localidadeService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _localidadeService.GetById(id);

            if (Resposta is null)
            {
                return NotFound();
            }
            return Ok(Resposta);
        }

        [HttpGet("cidade/{city}")]
        public async Task<ActionResult> GetByCity(string city)
        {
            return Ok(await _localidadeService.GetByCity(city));
        }

        [HttpGet("estado/{state}")]
        public async Task<ActionResult> GetByState(string state)
        {
            return Ok(await _localidadeService.GetByCity(state));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Localidade localidade)
        {
            if (localidade.Id == 0)
            {
                return BadRequest("Id do Produto é inválido!");
            }

            var validarPostagem = await _localidadeValidator.ValidateAsync(localidade);

            if (!validarPostagem.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);
            }

            var Resposta = await _localidadeService.Update(localidade);

            if (Resposta is null)
            {
                return NotFound("Localidade/ID não encontrados!");
            }
            return Ok(Resposta);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Localidade localidade)
        {
            var validarLocalidade = await _localidadeValidator.ValidateAsync(localidade);

            if (!validarLocalidade.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarLocalidade);
            }

            var Resposta = await _localidadeService.Create(localidade);

            if (Resposta is null)
            {
                return BadRequest("Não foi possível cadastrar essa localidade!");
            }

            return CreatedAtAction(nameof(GetById), new { id = localidade.Id }, localidade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var encontrarLocalidade = await _localidadeService.GetById(id);

            if (encontrarLocalidade is null)
            {
                return NotFound("Localidade não encontrada.");
            }
            await _localidadeService.Delete(encontrarLocalidade);
            return NoContent();
        }

    }

}
