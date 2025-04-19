using ComedorInfantil.Gestion.Application.DTOs.Inventory;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetAllInventoy
{
    public interface IGetAllInventoyQuerie
    {
        Task<List<InventoryDTO>> Execute();
    }
}
