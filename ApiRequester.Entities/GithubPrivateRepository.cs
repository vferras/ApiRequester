namespace ApiRequester.Entities
{
    using Newtonsoft.Json.Linq;

    public class GithubPrivateRepository
    {
        public GithubPrivateRepository(JObject jsonRepo)
        {
            Name = jsonRepo["name"].Value<string>();
        }

        public string Name { get; set; }
    }
}
