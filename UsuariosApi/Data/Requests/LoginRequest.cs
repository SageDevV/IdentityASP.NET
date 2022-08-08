using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    //Modelo de entrada de dados para requisições de login.
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
