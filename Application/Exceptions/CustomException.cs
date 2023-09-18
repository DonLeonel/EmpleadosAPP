using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Exceptions
{
    public class CustomException : Exception
    {
        public string ErrorMsj { set; get; }
        public bool Ok { set; get; }

        public CustomException(string error, bool ok)
        {
            this.Ok = ok;
            this.ErrorMsj = error;
        }
    }
}
