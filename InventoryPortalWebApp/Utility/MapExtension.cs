using System.Linq;

namespace InventoryPortalWebApp.Utility
{
    public static class MapExtension
    {
        /// <summary>
        /// Maps InventoryRequestModel into an array of FulfilllmentRequestModel
        /// </summary>
        /// <param name="fromModel"></param>
        /// <returns></returns>
        public static Model.FulfillmentRequestModel[] ToFulfillmentRequestModel(this Model.InventoryRequestModel fromModel)
        {
            return fromModel.Requests.Join(fromModel.Inventory, r => r.InventoryId, i => i.Id, (r, i) => new Model.FulfillmentRequestModel
            {
                Id = r.Id,
                InventoryId = r.InventoryId,
                Name = i.Name,
                Requested = r.RequestedKernels,
                Available = i.Kernels
            }).ToArray();
        }
    }
}
