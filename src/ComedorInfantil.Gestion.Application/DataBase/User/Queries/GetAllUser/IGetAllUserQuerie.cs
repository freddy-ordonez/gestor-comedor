using ComedorInfantil.Gestion.Application.DTOs.User;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetAllUser
{
    public interface IGetAllUserQuerie
    {
        /// <summary>
        ///  En este metodo vamos a poder traer todos los usuarios que esten activos.
        /// </summary>
        /// <returns>
        /// Retorna una lista de usuarios o una lista vacia.
        /// </returns>
        Task<List<UserDTO>> Execute();
    }
}
