using InventoryPortalWebApp.Model;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryPortalWebApp.Service
{
    public class FileDataService : IRequestsProvider
    {
        private readonly JsonSerializerOptions caseInsensitive = new() { PropertyNameCaseInsensitive = true };

        public async Task<InventoryRequestModel> GetRequests()
        {
            string result = await System.IO.File.ReadAllTextAsync(@"inventory.json");
            return JsonSerializer.Deserialize<InventoryRequestModel>(result, caseInsensitive);
        }
    }
}
