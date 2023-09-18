using Application.ConfigFecha;
using Application.Dtos.Inteface;
using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class EmpleadoResp : IResponseDTO
    {
        public Guid EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }

        [JsonConverter(typeof(CustomDateConvert))]
        public DateTime FechaAlta { get; set; }

        public SucursalResp Sucursal { get; set; }
        public CargoResp Cargo { get; set; }
        public Guid JefeID { get; set; }
    }
}
