namespace ApiRequester.XCutting.Http
{
    using System.Net;

    public class RequestResult<T>
    {
        public string ErrorContent { get; internal set; }

        public HttpStatusCode HttpCode { get; internal set; }

        public string NextPage { get; set; }

        public T Result { get; set; }

        public bool IsSuccessResponse()
        {
            return HttpCode == HttpStatusCode.OK;
        }
    }
}
