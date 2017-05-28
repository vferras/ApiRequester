namespace ApiRequester.XCutting.Http.Clients
{
    using Entities;
    using Resources.Github;
    using Runner;
    using Services;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Client : IClient
    {
        private readonly IRequestRunner _requestRunner;

        public Client(IRequestRunner requestRunner)
        {
            _requestRunner = requestRunner;
        }

        public async Task<RequestResult<IEnumerable<GithubUser>>> GetGithubUsers(ProvidersEnum provider, string queryString = "")
        {
            var request = RequestFactory.GetRequest(provider);
            request.Resource = GithubResources.Users;

            return await _requestRunner.ExecuteRequestThenParseResult(request, provider, JsonConverterFromGithub.AsUsers, queryString);
        }

        public async Task<RequestResult<IEnumerable<GithubOrganization>>> GetGithubOrganizations(ProvidersEnum provider, string queryString = "")
        {
            var request = RequestFactory.GetRequest(provider);
            request.Resource = GithubResources.Organizations;

            return await _requestRunner.ExecuteRequestThenParseResult(request, provider, JsonConverterFromGithub.AsOrganizations, queryString);
        }

        public async Task<RequestResult<IEnumerable<GithubPulicRepository>>> GetGithubPublicRepositories(ProvidersEnum provider, string queryString = "")
        {
            var request = RequestFactory.GetRequest(provider);
            request.Resource = GithubResources.PublicRepos;

            return await _requestRunner.ExecuteRequestThenParseResult(request, provider, JsonConverterFromGithub.AsPublicRepositories, queryString);
        }

        public async Task<RequestResult<IEnumerable<GithubPrivateRepository>>> GetGithubPrivateRepositories(ProvidersEnum provider, string queryString = "")
        {
            var request = RequestFactory.GetRequest(provider);
            request.Resource = GithubResources.PrivateRepos;

            return await _requestRunner.ExecuteRequestThenParseResult(request, provider, JsonConverterFromGithub.AsPrivateRepositories, queryString);
        }
    }
}
