namespace ApiRequester.XCutting.Http.Runner
{
    using System;
    using System.Threading.Tasks;
    using Requests;

    public interface IRequestRunner
    {
        Task<RequestResult<T>> ExecuteRequestThenParseResult<T>(Request request, ProvidersEnum provider, Func<string, T> parseResponseContent, string queryString = "", string search = "") where T : class;
    }
}
