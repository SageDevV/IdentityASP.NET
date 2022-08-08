using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
        {
            //Chamada do serviço para realização da lógica do serviço de cadastro do usuário.
            Result resultado = _cadastroService.CadastraUsuario(createDto);

            //Retorno de endpoint baseada na biblioteca FluentResults.
            if (resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Successes);
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario ([FromQuery] AtivaContaRequest ativaContaRequest)
        {
            //Chamada do serviço para realização da lógica de ativação da conta do usuário.
            Result result = _cadastroService.AtivaContaUsuario(ativaContaRequest);

            //Retorno de endpoint baseada na biblioteca FluentResults.
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }
    }
}
