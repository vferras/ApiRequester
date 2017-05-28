namespace ApiRequester.Entities
{
    using Newtonsoft.Json.Linq;

    public class GithubPulicRepository
    {
        public GithubPulicRepository(JObject jsonRepo)
        {
            Name = jsonRepo["name"].Value<string>();
        }

        public string Name { get; set; }
    }
}
