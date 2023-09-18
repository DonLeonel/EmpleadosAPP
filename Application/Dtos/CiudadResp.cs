using Application.Dtos.Inteface;

namespace Application.Dtos
{
    public class CiudadResp : IResponseDTO
    {
        public Guid CiudadID { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
