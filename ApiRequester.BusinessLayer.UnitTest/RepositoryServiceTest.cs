namespace ApiRequester.BusinessLayer.UnitTest
{
    using System.Threading.Tasks;

    using Entities;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Newtonsoft.Json.Linq;
    using Services;
    using System.Collections.Generic;
    using XCutting.Http;
    using XCutting.Http.Clients;
    using Xunit;

    public class UsersServiceTest
    {
        [Fact]
        public async Task GetANumberWhenAskingTotalNumberOfUsers()
        {
            var client = new Mock<IClient>();
            var logger = new Mock<ILogger<GithubService>>();
            IEnumerable<GithubUser> users = new List<GithubUser>
            {
                new GithubUser(JObject.Parse(FakeGithubUsers))
            };

            var result = new RequestResult<IEnumerable<GithubUser>>
            {
                Result = users
            };

            var sut = new GithubService(client.Object, logger.Object);

            client.Setup(s => s.GetGithubUsers(ProvidersEnum.Github, string.Empty)).Returns(Task.FromResult(result));

            var totalUsers = await sut.GetTotalUsersNumber();

            Assert.IsType<int>(totalUsers);
        }

        [Fact]
        public async Task GetANumberWhenAskingTotalNumberOfPublicRepos()
        {
            var client = new Mock<IClient>();
            var logger = new Mock<ILogger<GithubService>>();
            IEnumerable<GithubPulicRepository> repos = new List<GithubPulicRepository>
            {
                new GithubPulicRepository(JObject.Parse(FakeGithubRepos))
            };

            var result = new RequestResult<IEnumerable<GithubPulicRepository>>
            {
                Result = repos
            };

            var sut = new GithubService(client.Object, logger.Object);

            client.Setup(s => s.GetGithubPublicRepositories(ProvidersEnum.Github, string.Empty)).Returns(Task.FromResult(result));

            var totalRepos = await sut.GetTotalPublicRepos();

            Assert.IsType<int>(totalRepos);
        }

        [Fact]
        public async Task GetANumberWhenAskingTotalNumberOfPrivateRepos()
        {
            var client = new Mock<IClient>();
            var logger = new Mock<ILogger<GithubService>>();
            IEnumerable<GithubPrivateRepository> repos = new List<GithubPrivateRepository>
            {
                new GithubPrivateRepository(JObject.Parse(FakeGithubRepos))
            };

            var result = new RequestResult<IEnumerable<GithubPrivateRepository>>
            {
                Result = repos
            };

            var sut = new GithubService(client.Object, logger.Object);

            client.Setup(s => s.GetGithubPrivateRepositories(ProvidersEnum.Github, string.Empty)).Returns(Task.FromResult(result));

            var totalRepos = await sut.GetTotalPrivateRepos();

            Assert.IsType<int>(totalRepos);
        }

        private string FakeGithubUsers => "{\"login\": \"ghost\"}";

        private string FakeGithubRepos => "{\"name\": \"stunning-umbrella\"}";
    }
}
