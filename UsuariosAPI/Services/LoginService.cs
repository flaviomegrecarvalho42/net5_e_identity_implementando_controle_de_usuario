using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signManager, TokenService tokenService)
        {
            _signManager = signManager;
            _tokenService = tokenService;
        }

        public Result ConectarUsuario(LoginRequest loginRequest)
        {
            var resultadoIdentity = _signManager
                .PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false);

            if (!resultadoIdentity.Result.Succeeded)
            {
                return Result.Fail("Login falhou");
            }

            IdentityUser<int> usuarioIdentity = _signManager
                .UserManager
                .Users
                .FirstOrDefault(usuario => usuario.NormalizedUserName == loginRequest.UserName.ToUpper());

            Token token = _tokenService.CreateToken(usuarioIdentity);

            return Result.Ok().WithSuccess(token.Value);
        }
    }
}
