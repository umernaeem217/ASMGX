using System.Net;

namespace ASMGX.DeepMed.Shared.Http
{
    public class Response<T> : IResponse<T>
    {
        public Response():this(default)
        {
            

        }

        public Response(T data = default, string message = "Success", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
            Date = DateTime.UtcNow;
        }

        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
