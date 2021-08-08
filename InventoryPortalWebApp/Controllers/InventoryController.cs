using InventoryPortalWebApp.Model;
using InventoryPortalWebApp.Service;
using InventoryPortalWebApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryPortalWebApp.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IRequestsProvider requestsService;
        private readonly ILogger<InventoryController> logger;

        public InventoryController(IRequestsProvider requestsService, ILogger<InventoryController> logger)
        {
            this.requestsService = requestsService;
            this.logger = logger;
        }

        [Route("pending-requests")]
        [HttpGet]
        public async Task<string> Get() => await GetPendingRequests();

        private async Task<string> GetPendingRequests()
        {
            InventoryRequestModel model = await requestsService.GetRequests();

            logger.LogInformation(JsonSerializer.Serialize(model));

            return JsonSerializer.Serialize(model.ToFulfillmentRequestModel());
        }
    }
}
