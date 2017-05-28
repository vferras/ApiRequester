namespace ApiRequester.Api.Controllers
{
    using Messages.Requests;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    [Route("api/Login")]
    public class LoginController : Controller
    {
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            return Ok();
        }
    }
}