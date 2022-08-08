using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> usuario)
        {
            //Definindo os direitos do usuário.
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            //Criando uma chave de autenticação.
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );

            //Instanciando um objeto que contém a chave de login e o algoritmo de segurança.
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            //Instanciando um objeto JwtSecurityToken passando via contrutor os privilégios, credenciais, e um tempo de experição do token.
            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
                );

            //Convertendo a criptografia em uma string, e retornando um modelo instanciado de um token.
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
