namespace ApiRequester.Api.Fake.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BusinessLayer.Interfaces;
    using Messages.Responses.Cards;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    [Route("api")]
    public class FakeGithubController : Controller
    {
        private readonly Mock<IGithubService> _githubService;

        public FakeGithubController()
        {
            _githubService = new Mock<IGithubService>();
        }

        [HttpGet("github/cards")]
        [ResponseCache(CacheProfileName = "Default")]
        [ProducesResponseType(typeof(GithubStatsCards), 200)]
        public async Task<IActionResult> GetGithubCards()
        {
            var result = await GetGithubData();

            return Ok(result);
        }

        private async Task<GithubStatsCards> GetGithubData()
        {
            _githubService.Setup(s => s.GetTotalPublicRepos()).Returns(Task.FromResult(new Random().Next(60, 250)));
            _githubService.Setup(s => s.GetTotalPrivateRepos()).Returns(Task.FromResult(new Random().Next(50, 200)));
            _githubService.Setup(s => s.GetTotalForks()).Returns(Task.FromResult(new Random().Next(500, 800)));
            _githubService.Setup(s => s.GetTotalPullRequestsNb()).Returns(Task.FromResult(new Random().Next(50, 150)));
            _githubService.Setup(s => s.GetTotalUsersNumber()).Returns(Task.FromResult(new Random().Next(1500, 3000)));
            _githubService.Setup(s => s.GetTotalActiveUsersNb()).Returns(Task.FromResult(new Random().Next(700, 1800)));
            _githubService.Setup(s => s.GetTotalOrganizationsNumber()).Returns(Task.FromResult(new Random().Next(10, 50)));

            return new GithubStatsCards
            {
                ActiveUsers = await _githubService.Object.GetTotalActiveUsersNb().ConfigureAwait(false),
                Entities = await _githubService.Object.GetTotalOrganizationsNumber().ConfigureAwait(false),
                Forks = await _githubService.Object.GetTotalForks().ConfigureAwait(false),
                PrivateRepos = await _githubService.Object.GetTotalPrivateRepos().ConfigureAwait(false),
                PullRequests = await _githubService.Object.GetTotalPullRequestsNb().ConfigureAwait(false),
                PublicRepos = await _githubService.Object.GetTotalPublicRepos().ConfigureAwait(false),
                Users = await _githubService.Object.GetTotalUsersNumber().ConfigureAwait(false)
            };
        }
    }
}
