namespace ApiRequester.XCutting.Http.Requests
{
    using Resources.Github;
    using System.Collections.Generic;

    public class Request
    {
        public IDictionary<string, string> Headers { get; set; }

        public GithubResources Resource { get; set; }

        public Request()
        {
            Headers = new Dictionary<string, string>();
        }

        public string GetResource(string queryString = null, string search = null)
        {
            var queryStringExtension = "per_page=100" + (string.IsNullOrEmpty(queryString) ? "" : "&" + queryString);
            string resource;

            switch (Resource)
            {
                case GithubResources.Users:
                    resource = "/users?";
                    break;
                case GithubResources.Organizations:
                    resource = "/organizations?";
                    break;
                case GithubResources.PublicRepos:
                    resource = "/repositories?";
                    break;
                case GithubResources.PrivateRepos:
                    resource = "/user/repos?type=private&";
                    break;
                case GithubResources.PublicReposSearch:
                    resource = "/repos?";
                    break;
                case GithubResources.Forks:
                case GithubResources.PullRequests:
                    return "";
                default:
                    return string.Empty;
            }

            return resource + queryStringExtension;
        }
    }
}
