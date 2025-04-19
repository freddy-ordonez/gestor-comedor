using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Module;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Module.Queries.GetAllModule
{
    public class GetAllModuleQuerie : IGetAllModuleQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllModuleQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<ModuleDTO>> Exexute()
        {
            var listModules = await _dataBaseService.Modules.ToListAsync();
            return _mapper.Map<List<ModuleDTO>>(listModules);   
        }
    }
}
