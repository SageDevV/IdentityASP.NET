using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest request)
        {
            //Chamando serviço que contém logica de logar o usuário.
            Result resultado = _loginService.LogaUsuario(request);

            //Caso haver falha no serviço, é retornando um Unauthorized.
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);

            //Caso ao contrário é retornado um Success.
            return Ok(resultado.Successes);
        }
    }
}
