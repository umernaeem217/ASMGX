using System.Net;

namespace ASMGX.DeepMed.Shared.Http
{
    public interface IResponse<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
