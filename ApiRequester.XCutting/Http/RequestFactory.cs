namespace ApiRequester.XCutting.Http
{
    using Requests;

    public class RequestFactory
    {
        public static Request GetRequest(ProvidersEnum provider)
        {
            switch(provider)
            {
                case ProvidersEnum.Confluence:
                    return new ConfluenceRequest();
                case ProvidersEnum.Github:
                    return new GithubRequest();
                default:
                    return null;
            }
        }
    }
}
