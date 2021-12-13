using FilmesAPI.Data.DTO.Gerente;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private GerenteService _gerenteService;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionarGerente(CreateGerenteDto createGerenteDto)
        {
            ReadGerenteDto readGerenteDto = _gerenteService.AdicionarGerente(createGerenteDto);
            return CreatedAtAction(nameof(RecuperarGerentesPorId), new { readGerenteDto.Id }, readGerenteDto);
        }

        [HttpGet]
        public IActionResult RecuperarGerentes()
        {
            List<ReadGerenteDto> readGerenteDtos = _gerenteService.RecuperarGerentes();

            if (!readGerenteDtos.Any())
            {
                return NotFound();
            }

            return Ok(readGerenteDtos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarGerentesPorId(int id)
        {
            ReadGerenteDto readGerenteDto = _gerenteService.RecuperarGerentesPorId(id);

            if (readGerenteDto == null || readGerenteDto.Id == 0)
            {
                return NotFound();
            }

            return Ok(readGerenteDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarGerente(int id, [FromBody] UpdateGerenteDto updateGerenteDto)
        {
            Result resultado = _gerenteService.AtualizarGerente(id, updateGerenteDto);

            if (resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarGerente(int id)
        {
            Result resultado = _gerenteService.DeletarGerente(id);

            if (resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
