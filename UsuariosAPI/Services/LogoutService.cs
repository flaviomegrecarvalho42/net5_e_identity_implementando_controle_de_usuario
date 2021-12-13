using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signManager;

        public LogoutService(SignInManager<IdentityUser<int>> signManager)
        {
            _signManager = signManager;
        }

        public Result DesconectarUsuario()
        {
            var resultadoIdentity = _signManager.SignOutAsync();

            if (!resultadoIdentity.IsCompletedSuccessfully)
            {
                return Result.Fail("Logout falhou");
            }

            return Result.Ok();
        }
    }
}
