using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Inventory;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetInventoryById
{
    public class GetInventoryByIdQuerie : IGetInventoryByIdQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetInventoryByIdQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<InventoryDTO> Execute(int inventoryId)
        {
            var inventory = await _dataBaseService.Inventories.FirstOrDefaultAsync(x => x.InventoryId == inventoryId);
            return _mapper.Map<InventoryDTO>(inventory);
        }
    }
}
