namespace ComedorInfantil.Gestion.Application.DTOs.Inventory
{
    public class CreateInventoryDTO
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
