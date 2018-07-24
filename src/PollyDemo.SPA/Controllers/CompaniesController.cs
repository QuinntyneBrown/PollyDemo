using Microsoft.AspNetCore.Mvc;
using PollyDemo.SPA.Clients;
using System.Net.Http;
using System.Threading.Tasks;
using PollyDemo.Core.Extensions;

namespace PollyDemo.SPA.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController
    {
        private readonly CompaniesClient _client;
        private readonly IHttpClientFactory _factory;

        public CompaniesController(CompaniesClient client, IHttpClientFactory factory) {
            _client = client;
            _factory = factory;
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
        {            
            return await _factory.CreateClient("companies")
                .GetAsync<dynamic>("api/companies");
            
            return await _client.Get();
        }
    }
}
