using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.SPA.Clients
{
    public class CompaniesClient
    {
        private readonly HttpClient _client;
        public CompaniesClient(HttpClient client)
            => _client = client;

        public async Task<HttpResponseMessage> Get()
            => await _client.GetAsync("api/companies");
    }
}
