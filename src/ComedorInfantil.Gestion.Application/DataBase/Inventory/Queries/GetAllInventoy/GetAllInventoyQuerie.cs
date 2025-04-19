using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Inventory;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetAllInventoy
{
    public class GetAllInventoyQuerie : IGetAllInventoyQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllInventoyQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<InventoryDTO>> Execute()
        {
            var listInventory = await _dataBaseService.Inventories.ToListAsync();
            return _mapper.Map<List<InventoryDTO>>(listInventory);
        }
    }
}
