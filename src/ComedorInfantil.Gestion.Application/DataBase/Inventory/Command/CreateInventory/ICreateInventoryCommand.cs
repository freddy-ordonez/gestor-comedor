using ComedorInfantil.Gestion.Application.DTOs.Inventory;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.CreateInventory
{
    public interface ICreateInventoryCommand
    {
        Task<InventoryDTO> Execute(CreateInventoryDTO model, int userId);
    }
}
