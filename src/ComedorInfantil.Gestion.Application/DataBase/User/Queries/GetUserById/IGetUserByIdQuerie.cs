using ComedorInfantil.Gestion.Application.DTOs.User;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserById
{
    public interface IGetUserByIdQuerie
    {
        /// <summary>
        /// Este metodo trae un usuario de la base de datos.
        /// </summary>
        /// <param name="userId">El id del usuario que quiere traer de la base de datos</param>
        /// <returns>Retorna un usuario o null</returns>
        Task<UserDTO> Execute(int userId);
    }
}
