using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;
using UsuariosApi.Services;

public class LoginService
{
    private SignInManager<IdentityUser<int>> _signInManager;
    private TokenService _tokenService;

    public LoginService(SignInManager<IdentityUser<int>> signInManager,
        TokenService tokenService)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public Result LogaUsuario(LoginRequest request)
    {
        //Servi�o de verifica��o se cont�m um cadastro do usu�rio passando as credenciais do mesmo por argumento.
        var resultadoIdentity = _signInManager
            .PasswordSignInAsync(request.Username, request.Password, false, false);

        //Caso houver sucesso e conter um usu�rio devidamente cadastrado, � gerado um token pelo token service.
        if (resultadoIdentity.Result.Succeeded)
        {
            //Consulta de um usu�rio para passar um identityUser na cria��o de um token.
            var identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(usuario =>
                usuario.NormalizedUserName == request.Username.ToUpper());
            Token token = _tokenService.CreateToken(identityUser);
            return Result.Ok().WithSuccess(token.Value);
        }

        return Result.Fail("Login falhou");
    }
}
