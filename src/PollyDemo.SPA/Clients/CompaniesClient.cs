using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PollyDemo.Core.Extensions;

namespace PollyDemo.SPA.Clients
{
    public class CompaniesClient
    {
        private readonly HttpClient _client;
        public CompaniesClient(HttpClient client)
            => _client = client;

        public async Task<ActionResult<dynamic>> Get()
            => await _client.GetAsync<dynamic>("api/companies");
    }
}
