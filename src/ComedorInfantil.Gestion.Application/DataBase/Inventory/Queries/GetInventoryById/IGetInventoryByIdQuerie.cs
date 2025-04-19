using ComedorInfantil.Gestion.Application.DTOs.Inventory;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetInventoryById
{
    public interface IGetInventoryByIdQuerie
    {
        Task<InventoryDTO> Execute(int inventoryId);
    }
}
