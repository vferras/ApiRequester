namespace ApiRequester.BusinessLayer.Interfaces
{
    using System.Threading.Tasks;

    public interface IGithubService
    {
        Task<int> GetTotalPublicRepos();

        Task<int> GetTotalPrivateRepos();

        Task<int> GetTotalForks();

        Task<int> GetTotalPullRequestsNb();

        Task<int> GetTotalUsersNumber();

        Task<int> GetTotalActiveUsersNb();

        Task<int> GetTotalOrganizationsNumber();
    }
}
