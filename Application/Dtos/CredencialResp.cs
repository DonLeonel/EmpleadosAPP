using Application.Dtos.Inteface;

namespace Application.Dtos
{
    public class CredencialResp : IResponseDTO
    { 
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Avatar { get; set; }
    }
}
