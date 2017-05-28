namespace ApiRequester.BusinessLayer.Interfaces
{
    using System.Threading.Tasks;

    public interface IConfluenceService
    {
        Task<int> GetStatisticsPerEntity();

        Task<int> GetTotalUsersNb();

        Task<int> GetTotalActiveUsersNb();

        Task<int> GetTotalEntitiesNb();
    }
}
