namespace InventoryPortalWebApp.Model
{
    public class FulfillmentRequestModel
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public int Requested { get; set; }
        public int Available { get; set; }
    }
}
