using CinemasAPI.Services;
using FilmesAPI.Data.DTO.Cinema;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpPost]
        public IActionResult AdicionarCinema([FromBody] CreateCinemaDto createCinemaDto)
        {
            ReadCinemaDto readCinemaDto = _cinemaService.AdicionarCinema(createCinemaDto);
            return CreatedAtAction(nameof(RecuperarCinemasPorId), new { readCinemaDto.Id }, readCinemaDto);
        }

        [HttpGet]
        public IActionResult RecuperarCinemas([FromQuery] string nomeDoFilme)
        {
            List<ReadCinemaDto> readCinemaDtos = _cinemaService.RecuperarCinemas(nomeDoFilme);

            if (!readCinemaDtos.Any())
            {
                return NotFound();
            }

            return Ok(readCinemaDtos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarCinemasPorId(int id)
        {
            ReadCinemaDto readCinemaDto = _cinemaService.RecuperarCinemasPorId(id);

            if (readCinemaDto == null || readCinemaDto.Id == 0)
            {
                return NotFound();
            }

            return Ok(readCinemaDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCinema(int id, [FromBody] UpdateCinemaDto updateCinemaDto)
        {
            Result resultado = _cinemaService.AtualizarCinema(id, updateCinemaDto);

            if(resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCinema(int id)
        {
            Result resultado = _cinemaService.DeletarCinema(id);

            if (resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
