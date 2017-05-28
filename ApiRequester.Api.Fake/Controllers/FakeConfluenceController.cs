namespace ApiRequester.Api.Fake.Controllers
{
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    using BusinessLayer.Interfaces;
    using Messages.Responses;
    using Messages.Responses.Cards;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    [Route("api")]
    public class FakeConfluenceController : Controller
    {
        private readonly Mock<IConfluenceService> _confluenceService;

        public FakeConfluenceController()
        {
            _confluenceService = new Mock<IConfluenceService>();
        }

        [ResponseCache(CacheProfileName = "Default")]
        [HttpGet("confluence/entities/stats")]
        [ProducesResponseType(typeof(IEnumerable<ConfluenceStats>), 200)]
        public async Task<IActionResult> GetConfluenceStats()
        {
            var result = await GetConfluenceFakeData();

            return Ok(result);
        }

        [ResponseCache(CacheProfileName = "Default")]
        [HttpGet("confluence/cards")]
        [ProducesResponseType(typeof(ConfluenceStatsCards), 200)]
        public async Task<IActionResult> GetConfluenceCards()
        {
            var result = await GetConfluenceCardFakeData();

            return Ok(result);
        }

        private async Task<ConfluenceStatsCards> GetConfluenceCardFakeData()
        {
            _confluenceService.Setup(s => s.GetTotalUsersNb()).Returns(Task.FromResult(new Random().Next(1500, 3000)));
            _confluenceService.Setup(s => s.GetTotalActiveUsersNb()).Returns(Task.FromResult(new Random().Next(700, 1800)));
            _confluenceService.Setup(s => s.GetTotalEntitiesNb()).Returns(Task.FromResult(new Random().Next(10, 50)));

            return new ConfluenceStatsCards
            {
                ActiveUsersNb = await _confluenceService.Object.GetTotalActiveUsersNb().ConfigureAwait(false),
                EntitiesNb = await _confluenceService.Object.GetTotalEntitiesNb().ConfigureAwait(false),
                UsersNb = await _confluenceService.Object.GetTotalUsersNb().ConfigureAwait(false)
            };
        }

        private async Task<IEnumerable<ConfluenceStats>> GetConfluenceFakeData()
        {
            _confluenceService.Setup(s => s.GetStatisticsPerEntity()).Returns(Task.FromResult(GetConfluenceStatsDto()));

            var confluenceStatsDto = await _confluenceService.Object.GetStatisticsPerEntity();

            var confluenceStats = new ConfluenceStats
            {
                ActiveUsersNb = confluenceStatsDto,
                SpacesNb = confluenceStatsDto,
                EntityName = "",
                UsersNb = confluenceStatsDto
            };

            var list = new List<ConfluenceStats> {confluenceStats};

            return list;
        }

        private int GetConfluenceStatsDto()
        {
            return new Random().Next(10, 500);
        }
    }
}
