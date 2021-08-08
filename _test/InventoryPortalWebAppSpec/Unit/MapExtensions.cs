using InventoryPortalWebApp.Model;
using InventoryPortalWebApp.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InventoryPortalWebAppSpec.Unit
{
    [TestClass]
    public class MapExtensions
    {
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
            };
            requests = new RequestModel[]
            {
                new() { Id = 0, InventoryId = inventory[1].Id, RequestedKernels = 300 },
                new() { Id = 1, InventoryId = inventory[0].Id, RequestedKernels = 4000 },
            };
            inputModel = new()
            {
                Inventory = inventory,
                Requests = requests
            };
        }

        [TestMethod]
        public void MapsInventoryRequestModelToFulfillment()
        {
            FulfillmentRequestModel[] result = inputModel.ToFulfillmentRequestModel();

            Assert.AreEqual(inputModel.Inventory[1].Name, result[0].Name);
            Assert.AreEqual(inputModel.Inventory[0].Name, result[1].Name);
            Assert.AreEqual(4000, result[1].Requested);
            Assert.AreEqual(inputModel.Inventory[0].Kernels, result[1].Available);
        }
    }
}
