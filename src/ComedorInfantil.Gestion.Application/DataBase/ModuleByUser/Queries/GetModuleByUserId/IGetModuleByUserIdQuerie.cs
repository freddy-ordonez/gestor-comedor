using ComedorInfantil.Gestion.Application.DTOs.Module;
using ComedorInfantil.Gestion.Application.DTOs.ModuleByUser;

namespace ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Queries.GetModuleByUserId
{
    public interface IGetModuleByUserIdQuerie
    {
        Task<List<ModuleByUserDTO>> Execute(int userId);
    }
}
