namespace ApiRequester.Entities
{
    using Newtonsoft.Json.Linq;

    public class GithubOrganization
    {
        public GithubOrganization(JObject jsonUser)
        {
            Login = jsonUser["login"].Value<string>();
        }

        public string Login { get; set; }

        public string Description { get; set; }
    }
}
