using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult DeslogaUsuario()
        {
            //Chamada do serviço de deslogar usuário.
            Result resultado = _logoutService.DeslogaUsuario();

            //Caso o resultado não for válido é retornado um não autorizado.
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
    }
}
