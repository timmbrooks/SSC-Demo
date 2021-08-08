using InventoryPortalWebApp.Model;
using System.Threading.Tasks;

namespace InventoryPortalWebApp.Service
{
    public interface IRequestsProvider
    {
        Task<InventoryRequestModel> GetRequests();
    }
}
