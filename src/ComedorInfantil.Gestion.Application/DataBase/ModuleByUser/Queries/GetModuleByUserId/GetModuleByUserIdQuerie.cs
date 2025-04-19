using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Module;
using ComedorInfantil.Gestion.Application.DTOs.ModuleByUser;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Queries.GetModuleByUserId
{
    public class GetModuleByUserIdQuerie : IGetModuleByUserIdQuerie
    {
        private readonly IDataBaseService _dataBaseService;

        public GetModuleByUserIdQuerie(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        public async Task<List<ModuleByUserDTO>> Execute(int userId)
        {
            var listModuleByUser = await (from moduleByUser in _dataBaseService.ModuleForUsers
                                          join module in _dataBaseService.Modules
                                          on moduleByUser.ModuleId equals module.ModuleId
                                          where moduleByUser.UserId == userId
                                          select new ModuleByUserDTO
                                          {
                                              ModuleId = module.ModuleId,
                                              ClassCSS = module.ClassCSS,
                                              Link = module.Link,
                                              ModuleName = module.ModuleName,
                                              ModuleForUserId = moduleByUser.ModuleForUserId,
                                              UserId = moduleByUser.UserId
                                          }
                                          ).ToListAsync();
            return listModuleByUser;
        }
    }
}
