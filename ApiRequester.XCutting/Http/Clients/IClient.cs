namespace ApiRequester.XCutting.Http.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Entities;

    public interface IClient
    {
        Task<RequestResult<IEnumerable<GithubUser>>> GetGithubUsers(ProvidersEnum provider, string queryString = "");

        Task<RequestResult<IEnumerable<GithubOrganization>>> GetGithubOrganizations(ProvidersEnum provider, string queryString = "");

        Task<RequestResult<IEnumerable<GithubPulicRepository>>> GetGithubPublicRepositories(ProvidersEnum provider, string queryString = "");

        Task<RequestResult<IEnumerable<GithubPrivateRepository>>> GetGithubPrivateRepositories(ProvidersEnum provider, string queryString = "");
    }
}
