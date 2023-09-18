using Application.Dtos.Inteface;
using System.Net;

namespace Application.Dtos
{
    public class RespBase : IResponseDTO
    {
        public string ErrorMsj { get; set; }
        public HttpStatusCode Status { get; set; }
        public bool Ok { get; set; } = true;

        public void SetErrorMsj(string msj)
        {
            ErrorMsj = msj;
            Ok = false;
        }
    }
}
