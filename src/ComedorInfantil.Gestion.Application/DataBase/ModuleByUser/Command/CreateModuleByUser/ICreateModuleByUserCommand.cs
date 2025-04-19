using ComedorInfantil.Gestion.Application.DTOs.ModuleByUser;

namespace ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.CreateModuleByUser
{
    public interface ICreateModuleByUserCommand
    {
        Task<ModuleByUserDTO> Execute(CreateModuleByUserDTO model, int userId);
    }
}
