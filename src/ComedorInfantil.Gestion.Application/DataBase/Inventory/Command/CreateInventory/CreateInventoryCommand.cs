using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Inventory;
using ComedorInfantil.Gestion.Domain.Entities.Inventory;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.CreateInventory
{
    public class CreateInventoryCommand : ICreateInventoryCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateInventoryCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<InventoryDTO> Execute(CreateInventoryDTO model, int userId)
        {
            var newInventory = _mapper.Map<InventoryEntity>(model);

            _dataBaseService.Inventories.Add(newInventory);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto el inventario con nombre de: {model.ProductName} y id: {newInventory.InventoryId}",
            };

            await _createAuditCommand.Execute(newAudit);

            return _mapper.Map<InventoryDTO>(newInventory);
        }
    }
}
