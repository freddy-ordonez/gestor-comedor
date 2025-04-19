using ComedorInfantil.Gestion.Application.DTOs.Module;

namespace ComedorInfantil.Gestion.Application.DataBase.Module.Queries.GetAllModule
{
    public interface IGetAllModuleQuerie
    {
        Task<List<ModuleDTO>> Exexute();
    }
}
