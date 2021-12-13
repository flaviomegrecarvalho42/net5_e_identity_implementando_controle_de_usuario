using FilmesAPI.Data.DTO.Sessao;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionarSessao(CreateSessaoDto createSessaoDto)
        {
            ReadSessaoDto readSessaoDto = _sessaoService.AdicionarSessao(createSessaoDto);
            return CreatedAtAction(nameof(RecuperarSessoesPorId), new { readSessaoDto.Id }, readSessaoDto);
        }

        [HttpGet]
        public IActionResult RecuperarSessoes()
        {
            List<ReadSessaoDto> readSessaoDtos = _sessaoService.RecuperarSessoes();

            if (!readSessaoDtos.Any())
            {
                return NotFound();
            }

            return Ok(readSessaoDtos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarSessoesPorId(int id)
        {
            ReadSessaoDto readSessaoDto = _sessaoService.RecuperarSessoesPorId(id);

            if (readSessaoDto == null || readSessaoDto.Id == 0)
            {
                return NotFound();
            }

            return Ok(readSessaoDto);
        }

        #region Criar os métodos para atualizar e deletar sessão
        //[HttpPut("{id}")]
        //public IActionResult AtualizarGerente(int id, [FromBody] UpdateGerenteDto updateGerenteDto)
        //{
        //    Result resultado = _gerenteService.AtualizarGerente(id, updateGerenteDto);

        //    if (resultado.IsFailed)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeletaGerente(int id)
        //{
        //    Result resultado = _gerenteService.DeletarGerente(id);

        //    if (resultado.IsFailed)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
        #endregion
    }
}
