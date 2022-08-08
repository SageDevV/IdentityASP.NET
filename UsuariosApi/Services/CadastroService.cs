using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private readonly EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            //Dê para de um dto para uma entidade.
            Usuario usuario = _mapper.Map<Usuario>(createDto);

            //Dê para de uma entidade para um usuário identity.
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            //Criando um registro passando o usuário identity passando o password que veio do request.
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            if (resultadoIdentity.Result.Succeeded)
            {
                //Caso haver sucesso na criação, é chamado o serviço de geração de token para confirmação email, passando o usuário cadastrado.
                Task<string> code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity);

                //Garantir que essa Url não sofrerá nenhum tipo de alteração/Conversão no processo de envio de email com esse token.
                var encodeCode = HttpUtility.UrlEncode(code.Result);

                //Invocando serviço que dispara e-mail para o usuário, passando o destinatário, assunto do e-mail e o id do usuário.
                _emailService.CrirEmail(new[] { usuarioIdentity .Email}, "Link de ativação", usuarioIdentity.Id, encodeCode);

                return Result.Ok().WithSuccess(code.Result);
            }
            return Result.Fail("Falha ao cadastrar usuário");

        }

        public Result AtivaContaUsuario(AtivaContaRequest ativaContaRequest)
        {
            //Buscando o usuário baseado no request.
            IdentityUser<int> identityUser = _userManager.Users.FirstOrDefault(u => u.Id == ativaContaRequest.UsuarioId);

            //Confirmando o email do usuário passando o usuário retornado da busca, e o código de ativação vindo do request.
            Task<IdentityResult> identityResult = _userManager.ConfirmEmailAsync(identityUser, ativaContaRequest.CodigoDeAtivacao);

            //Caso houver sucesso na confirmação do Email, retornamos um Ok.
            if (identityResult.Result.Succeeded) return Result.Ok();

            //Caso ao contrário, falha.
            return Result.Fail("Falha ao ativar conta do usuário.");
        }
    }
}
