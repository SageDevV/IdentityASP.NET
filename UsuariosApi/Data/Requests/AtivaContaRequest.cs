using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    //Modelo de request FromQuery
    public class AtivaContaRequest
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}
