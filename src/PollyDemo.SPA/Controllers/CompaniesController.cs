using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PollyDemo.SPA.Clients;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace PollyDemo.SPA.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController
    {
        private readonly CompaniesClient _client;
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompaniesController(CompaniesClient client, IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor) {
            _client = client;
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
        {
            var response = await _factory.CreateClient("companies").GetAsync("api/companies");

            response.Headers.TryGetValues("X-Retry-Count", out var r);
            
            _httpContextAccessor.HttpContext.Request.HttpContext.Response.Headers.Add("X-Retry-Count", r.First());

            return DeserializeObject((await response.Content.ReadAsStringAsync()));
            
            return await _client.Get();
        }
    }
}
