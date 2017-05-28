namespace ApiRequester.Api.Controllers
{
    using System.Threading.Tasks;

    using BusinessLayer.Interfaces;
    using Logging;
    using Messages.Responses.Cards;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]/[action]")]
    public class GithubController : Controller
    {
        private readonly IGithubService _githubService;
        private readonly ILogger _logger;

        public GithubController(IGithubService repositoriesService, ILogger<GithubController> logger)
        {
            _githubService = repositoriesService;
            _logger = logger;
        }

        [ResponseCache(CacheProfileName = "Default")]
        [ProducesResponseType(typeof(GithubStatsCards), 200)]
        public async Task<IActionResult> Cards()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Getting users");
            var result = await GetGithubData();

            return Ok(result); 
        }

        private async Task<GithubStatsCards> GetGithubData()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Getting Github cards data");
            return new GithubStatsCards
            {
                Users = await _githubService.GetTotalUsersNumber().ConfigureAwait(false),
                //Entities = await _githubService.GetTotalOrganizationsNumber().ConfigureAwait(false),
                PublicRepos = await _githubService.GetTotalPublicRepos().ConfigureAwait(false),
                PrivateRepos = await _githubService.GetTotalPrivateRepos().ConfigureAwait(false)
            };
        }
    }
}

