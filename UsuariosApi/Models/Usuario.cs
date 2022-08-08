
namespace UsuariosApi.Models
{
    //Modelo de dados da entidade usuário que se encontra no banco.
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
