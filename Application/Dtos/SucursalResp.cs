using Application.Dtos.Inteface;

namespace Application.Dtos
{
    public class SucursalResp : IResponseDTO
    {
        public Guid SucursalID { get; set; }
        public string Nombre { get; set; } = null!;
        public CiudadResp Ciudad { get; set; }
    }
}
