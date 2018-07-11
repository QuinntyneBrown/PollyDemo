using Microsoft.AspNetCore.Mvc;
using PollyDemo.SPA.Clients;
using System.Threading.Tasks;

namespace PollyDemo.SPA.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController
    {
        private readonly CompaniesClient _client;

        public CompaniesController(CompaniesClient client)
            => _client = client;

        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
            => await _client.Get();
    }
}
