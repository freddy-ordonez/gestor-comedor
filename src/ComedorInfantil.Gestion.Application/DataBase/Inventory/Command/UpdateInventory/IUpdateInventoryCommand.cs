using ComedorInfantil.Gestion.Application.DTOs.Inventory;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.UpdateInventory
{
    public interface IUpdateInventoryCommand
    {
        Task<bool> Execute(UpdateInventoryDTO model, int inventoryId, int userId);
    }
}
