using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.DTO;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController: ControllerBase
    {
        private readonly CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Result resultado = _cadastroService.CadastrarUsuario(createUsuarioDto);

            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }

            return Ok(resultado.Successes);
        }

        [HttpGet]
        [Route("/Ativar")]
        public IActionResult AtivarContaUsuario([FromQuery]AtivaContaRequest ativaContaRequest)
         {
            Result resultado = _cadastroService.AtivarContaUsuario(ativaContaRequest);

            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }

            return Ok(resultado.Successes);
        }
    }
}
