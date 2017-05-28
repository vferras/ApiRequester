namespace ApiRequester.XCutting.Http.Runner
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Requests;
    using RestSharp;
    using RetryPolicies;

    public class RequestRunner : IRequestRunner
    {
        private readonly ILogger _logger;

        public RequestRunner(ILogger<RequestRunner> logger)
        {
            _logger = logger;
        }

        public async Task<RequestResult<T>> ExecuteRequestThenParseResult<T>(Request request, ProvidersEnum provider, Func<string, T> parseResponseContent,
            string queryString = "", string search = null) where T : class
        {
            return await ExecuteRequestAsync(request, provider, parseResponseContent, null, queryString, search).ConfigureAwait(false);
        }

        private async Task<RequestResult<T>> ExecuteRequestAsync<T>(Request request, ProvidersEnum provider, Func<string, T> parseResponseContent,
            Func<HttpStatusCode, T> errorFunc, string queryString = null, string search = null) where T : class
        {
            var result = new RequestResult<T>();

            var rsRequest = new RestRequest
            {
                Resource = request.GetResource(queryString, search),
                Method = Method.GET
            };

            if (request.Headers.Count > 0)
            {
                request.Headers.Keys.ToList().ForEach(key =>
                {
                    rsRequest.AddHeader(key, request.Headers[key]);
                });
            }

            var requestResult =  await RetryPolicyFactory.GetPolicy(RetryPolicyEnum.SimpleRetry, 5).
                ExecuteAsync(async () => await GetResponseContentAsync(rsRequest, provider).ConfigureAwait(false)).ConfigureAwait(false);
            _logger.LogDebug(requestResult.Content);
            if (!IsSuccessful(requestResult))
            {
                if (errorFunc != null)
                {
                    result.Result = errorFunc(result.HttpCode);
                    if (result.Result != null)
                    {
                        if (requestResult.Content != null)
                        {
                            _logger.LogInformation($"*** GITHUB RESPONSE = {requestResult.Content}***");
                        }

                        result.HttpCode = HttpStatusCode.OK;

                        return result;
                    }
                }
            }

            if (parseResponseContent == null)
            {
                return result;
            }

            try
            {
                result.Result = parseResponseContent(requestResult.Content);
                var header = requestResult.Headers.SingleOrDefault(h => h.Name == "Link");
                result.NextPage = header == null ? string.Empty : (string)header.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                _logger.LogError(ex.Message);
                throw;
            }

            return result;
        }

        private static Task<IRestResponse> GetResponseContentAsync(IRestRequest request, ProvidersEnum provider)
        {
            IRestClient client;

            switch (provider)
            {
                case ProvidersEnum.Github:
                    client = new RestClient(Settings.GithubUrl);
                    break;
                default:
                    client = new RestClient(Settings.GithubUrl);
                    break;
            }

            var tcs = new TaskCompletionSource<IRestResponse>();
            
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
        private static bool IsSuccessful(IRestResponse response)
        {
            return response.StatusCode == HttpStatusCode.OK;
        }

    }
}
