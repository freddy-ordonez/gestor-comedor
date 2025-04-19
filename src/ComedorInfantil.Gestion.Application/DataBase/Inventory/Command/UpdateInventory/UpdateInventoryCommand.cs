using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Inventory;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.UpdateInventory
{
    public class UpdateInventoryCommand : IUpdateInventoryCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public UpdateInventoryCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(UpdateInventoryDTO model, int inventoryId, int userId)
        {
            var inventory = await _dataBaseService.Inventories.FirstOrDefaultAsync(x => x.InventoryId == inventoryId);
            if (inventory == null)
                return false;

            _mapper.Map(model, inventory);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Actualizo el inventario con nombre de: {model.ProductName} y id: {inventoryId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
