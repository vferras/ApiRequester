namespace ApiRequester.XCutting.Http.Requests
{
    public class GithubRequest : Request
    {
        public GithubRequest()
        {
            Headers.Add("Authorization", "token xxxx");
        }
    }
}
