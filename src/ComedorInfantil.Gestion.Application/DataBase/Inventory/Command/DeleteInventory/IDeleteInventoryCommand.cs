namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.DeleteInventory
{
    public interface IDeleteInventoryCommand
    {
        Task<int> Execute(int inventoryId, int userId);
    }
}
