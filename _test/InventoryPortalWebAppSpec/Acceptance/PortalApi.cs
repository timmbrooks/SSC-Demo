using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging.Abstractions;

using System.Text.Json;
using System.Threading.Tasks;

using InventoryPortalWebApp.Controllers;
using InventoryPortalWebApp.Model;
using InventoryPortalWebApp.Service;

namespace InventoryPortalWebAppSpec.Acceptance
{
    [TestClass]
    public class PortalApi
    {
        IRequestsProvider service;
        private InventoryModel[] inventory;
        private RequestModel[] requests;
        private InventoryRequestModel inputModel;

        [TestInitialize]
        public void Setup()
        {
            inventory = new InventoryModel[]
            {
                new() { Id = 0, Name = "TYPE1", Kernels = 30 },
                new() { Id = 1, Name = "TYPE2", Kernels = 400 },
                new() { Id = 2, Name = "TYPE3", Kernels = 5000 },
            };
            requests = new RequestModel[]
            {
                new() { Id = 0, InventoryId = inventory[1].Id, RequestedKernels = 20 },
                new() { Id = 1, InventoryId = inventory[2].Id, RequestedKernels = 300 },
                new() { Id = 2, InventoryId = inventory[0].Id, RequestedKernels = 4000 },
            };
            inputModel = new()
            {
                Inventory = inventory,
                Requests = requests
            };

            service = new InventoryPortalWebApp.Service.Fakes.StubIRequestsProvider()
            {
                GetRequests = () => Task.FromResult(inputModel)
            };
        }

        [TestMethod]
        public void MapsInventoryDataFromExternalResource()
        {
            InventoryController controller = new(service, new NullLogger<InventoryController>());

            string result = controller.Get().Result;

            FulfillmentRequestModel[] model = JsonSerializer.Deserialize<FulfillmentRequestModel[]>(result);
            Assert.AreEqual(inputModel.Inventory[1].Name, model[0].Name);
            Assert.AreEqual(inputModel.Inventory[2].Name, model[1].Name);
            Assert.AreEqual(inputModel.Inventory[0].Name, model[2].Name);
            Assert.AreEqual(4000, model[2].Requested);
            Assert.AreEqual(inputModel.Inventory[0].Kernels, model[2].Available);
        }
    }
}
