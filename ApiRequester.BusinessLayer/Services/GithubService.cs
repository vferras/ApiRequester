namespace ApiRequester.BusinessLayer.Services
{
    using Entities;
    using Interfaces;
    using Logging;
    using Microsoft.Extensions.Logging;
    using XCutting.Http;
    using XCutting.Http.Clients;

    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class GithubService : IGithubService
    {
        private readonly IClient _client;
        private readonly ILogger _logger;

        public GithubService(IClient client, ILogger<GithubService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public Task<int> GetTotalActiveUsersNb()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalOrganizationsNumber()
        {
            var orgs = new List<GithubOrganization>();

            try
            {
                var data = await _client.GetGithubOrganizations(ProvidersEnum.Github).ConfigureAwait(false);
                while (!string.IsNullOrEmpty(data.NextPage) && data.NextPage.Contains("next"))
                {
                    data = await _client.GetGithubOrganizations(ProvidersEnum.Github, GetNextPage(data.NextPage)).ConfigureAwait(false);
                    orgs.AddRange(data.Result);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(LoggingEvents.GET_ITEMS_NUMBER, e, "An error occured while getting the total organization number");
                return 0;
            }

            return orgs.Count;
        }

        public Task<int> GetTotalForks()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalPullRequestsNb()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalPrivateRepos()
        {
            var repos = new List<GithubPrivateRepository>();

            try
            { 
                _logger.LogInformation(LoggingEvents.GET_ITEMS_NUMBER, "Getting the total users number");
                var data = await _client.GetGithubPrivateRepositories(ProvidersEnum.Github).ConfigureAwait(false);
                repos.AddRange(data.Result);

                while (!string.IsNullOrEmpty(data.NextPage) && data.NextPage.Contains("next"))
                {
                    data = await _client.GetGithubPrivateRepositories(ProvidersEnum.Github, GetNextPage(data.NextPage)).ConfigureAwait(false);
                    repos.AddRange(data.Result);
                }
            }   
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.GET_ITEMS_NUMBER, e, "An error occured while getting the total number of private repositories");
                return 0;
            }

            return repos.Count;
        }

        public async Task<int> GetTotalPublicRepos()
        {
            var repos = new List<GithubPulicRepository>();

            try
            { 
                var data = await _client.GetGithubPublicRepositories(ProvidersEnum.Github).ConfigureAwait(false);
                repos.AddRange(data.Result);

                while (!string.IsNullOrEmpty(data.NextPage) && data.NextPage.Contains("next"))
                {
                    data = await _client.GetGithubPublicRepositories(ProvidersEnum.Github, GetNextPage(data.NextPage)).ConfigureAwait(false);
                    repos.AddRange(data.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.GET_ITEMS_NUMBER, e, "An error occured while getting the total number of public repositories");
                return 0;
            }

            return repos.Count;
        }

        public async Task<int> GetTotalUsersNumber()
        {
            var users = new List<GithubUser>();

            try
            {
                var data = await _client.GetGithubUsers(ProvidersEnum.Github).ConfigureAwait(false);
                users.AddRange(data.Result);

                while (!string.IsNullOrEmpty(data.NextPage) && data.NextPage.Contains("next"))
                {
                    data = await _client.GetGithubUsers(ProvidersEnum.Github, GetNextPage(data.NextPage)).ConfigureAwait(false);
                    users.AddRange(data.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.GET_ITEMS_NUMBER, e, "An error occured while getting the total users number");
                return 0;
            }

            return users.Count;
        }

        private static string GetNextPage(string header)
        {
            return header.Split(',')[0].Contains("next")
                ? header.Split(',')[0].Split('?')[1].Split('&')[1].Split('>')[0]
                : header.Split(',')[1].Split('?')[1].Split('&')[1].Split('>')[0];
        }
    }
}
