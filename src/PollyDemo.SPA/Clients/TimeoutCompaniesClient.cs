using System.Net.Http;

namespace PollyDemo.SPA.Clients
{
    public class TimeoutCompaniesClient: CompaniesClient
    {
        public TimeoutCompaniesClient(HttpClient client)
            :base(client) { }
    }
}
