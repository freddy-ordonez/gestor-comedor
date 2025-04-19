using ComedorInfantil.Gestion.Application.DTOs.User;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.CreateUser
{
    public interface ICreateUserCommand
    {
        Task<UserDTO> Execute(CreateUserDTO model, int userId);
    }
}
