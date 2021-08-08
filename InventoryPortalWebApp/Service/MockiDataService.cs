using InventoryPortalWebApp.Model;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryPortalWebApp.Service
{
    public class MockiDataService : IRequestsProvider
    {
        private readonly RuntimeConfig config;
        private readonly JsonSerializerOptions caseInsensitive = new() { PropertyNameCaseInsensitive = true };

        public MockiDataService(IOptions<RuntimeConfig> config)
        {
            this.config = config.Value;
        }

        public async Task<InventoryRequestModel> GetRequests()
        {
            HttpClient client = new();
            string result = await client.GetStringAsync(config.RequestsEP);

            return JsonSerializer.Deserialize<InventoryRequestModel>(result, caseInsensitive);
        }
    }
}
