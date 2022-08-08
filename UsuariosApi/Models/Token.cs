namespace UsuariosApi.Models
{
    //Modelo de dados para criação de token.
    public class Token
    {
        public Token(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
