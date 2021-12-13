using FilmesAPI.Data.DTO.Endereco;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto createEnderecoDto)
        {
            ReadEnderecoDto readCinemaDto = _enderecoService.AdicionarEndereco(createEnderecoDto);
            return CreatedAtAction(nameof(RecuperarEnderecosPorId), new { readCinemaDto.Id }, readCinemaDto);
        }

        [HttpGet]
        public IActionResult RecuperarEnderecos()
        {
            List<ReadEnderecoDto> readEnderecoDtos = _enderecoService.RecuperarEnderecos();

            if (!readEnderecoDtos.Any())
            {
                return NotFound();
            }

            return Ok(readEnderecoDtos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarEnderecosPorId(int id)
        {
            ReadEnderecoDto readEnderecoDto = _enderecoService.RecuperarEnderecosPorId(id);

            if (readEnderecoDto == null || readEnderecoDto.Id == 0)
            {
                return NotFound();
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto updateEnderecoDto)
        {
            Result resultado = _enderecoService.AtualizarEndereco(id, updateEnderecoDto);

            if (resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarEndereco(int id)
        {
            Result resultado = _enderecoService.DeletarEndereco(id);

            if (resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}