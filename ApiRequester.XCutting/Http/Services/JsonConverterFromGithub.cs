namespace ApiRequester.XCutting.Http.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;
    using Newtonsoft.Json.Linq;

    public static class JsonConverterFromGithub
    {
        public static readonly Func<string, IEnumerable<GithubUser>> AsUsers = responseContent =>
        {
            var jsonResponse = JArray.Parse(responseContent);
            return jsonResponse.Select(user => new GithubUser((JObject) user)).ToList();
        };

        public static readonly Func<string, IEnumerable<GithubOrganization>> AsOrganizations = responseContent =>
        {
            var jsonResponse = JArray.Parse(responseContent);
            return jsonResponse.Select(org => new GithubOrganization((JObject)org)).ToList();
        };

        public static readonly Func<string, IEnumerable<GithubPulicRepository>> AsPublicRepositories = responseContent =>
        {
            var jsonResponse = JArray.Parse(responseContent);
            return jsonResponse.Select(repo => new GithubPulicRepository((JObject)repo)).ToList();
        };

        public static readonly Func<string, IEnumerable<GithubPrivateRepository>> AsPrivateRepositories = responseContent =>
        {
            var jsonResponse = JArray.Parse(responseContent);
            return jsonResponse.Select(repo => new GithubPrivateRepository((JObject)repo)).ToList();
        };
    }
}
