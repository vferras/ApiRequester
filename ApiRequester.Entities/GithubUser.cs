namespace ApiRequester.Entities
{
    using Newtonsoft.Json.Linq;

    public class GithubUser
    {
        public GithubUser(JObject jsonUser)
        {
            Login = jsonUser["login"].Value<string>();
        }

        public string Login { get; set; }
    }
}
