using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.Features.User;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserByEmailAndPassword
{
    public interface IGetUserByEmailAndPasswordQuerie
    {
        Task<AuthUserLogin> Execute(LoginUserDTO loginUserDTO);
    }
}
