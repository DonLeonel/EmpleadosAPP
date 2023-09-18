using Application.Dtos.Inteface;

namespace Application.Dtos
{
    public class CargoResp : IResponseDTO 
    {
        public Guid CargoID { get; set; }
        public string Nombre { get; set; } = null!;

    }
}
