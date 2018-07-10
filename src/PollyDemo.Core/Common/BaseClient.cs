using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace PollyDemo.Core.Common
{
    public class BaseClient<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly HttpClient _client;

        public BaseClient(HttpClient client, ILogger<T> logger)
        {
            _client = client;
            _logger = logger;
        }
    }
}
