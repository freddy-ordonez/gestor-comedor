using ComedorInfantil.Gestion.Application.DTOs.User;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdatePasswordUser
{
    public interface IUpdatePasswordUserCommand
    {
        Task<bool> Execute(UpdatePasswordUserDTO model, int userId, int userIdAudit);
    }
}
