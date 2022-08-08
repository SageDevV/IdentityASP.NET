using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result DeslogaUsuario()
        {
            //Chamado do serviço para deslogar.
            Task resultadoIdentity = _signInManager.SignOutAsync();

            //Caso o resultado tenha sido um sucesso, retorna um OK.
            if (resultadoIdentity.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }            
            return Result.Fail("Logout falhou");

        }
    }
}
