namespace ApiRequester.Messages.Responses.Cards
{
    using System;

    public class GithubRepositories
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Owner { get; set; }
    }
}
