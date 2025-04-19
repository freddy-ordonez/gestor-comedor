using ComedorInfantil.Gestion.Domain.Entities.InKindDonation;

//Reglas de Negocio:
//Vamos a poder crear un inventario desde(POST): /inventories
//Vamos a poder tomar todos los inventarios desde(GET): /inventories
//Vamos a poder actualizar un inventario desde(PUT): /inventories/{inventoryId}
//Vamos a poder eliminar un inventario desde(DELETE): /inventories/{inventoryId}
namespace ComedorInfantil.Gestion.Domain.Entities.Inventory
{
    public class InventoryEntity
    {
        public int InventoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public InKindDonationEntity InKindDonation { get; set; }
    }
}
