
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.DeleteInventory
{
    public class DeleteInventoryCommand : IDeleteInventoryCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteInventoryCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<int> Execute(int inventoryId, int userId)
        {
            var inventoryRelation = await _dataBaseService.InKindDonations.FirstOrDefaultAsync(x => x.ProductId == inventoryId);
            if (inventoryRelation != null)
                return -1;

            var inventory = await _dataBaseService.Inventories.FirstOrDefaultAsync(x => x.InventoryId == inventoryId);
            if (inventory == null)
                return 1;

            _dataBaseService.Inventories.Remove(inventory);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino el inventario con nombre de: {inventory.ProductName} y id: {inventoryId}",
            };

            await _createAuditCommand.Execute(newAudit);
            return 0;
        }
    }
}
