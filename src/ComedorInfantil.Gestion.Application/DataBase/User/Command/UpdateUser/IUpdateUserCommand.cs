using ComedorInfantil.Gestion.Application.DTOs.User;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdateUser
{
    public interface IUpdateUserCommand
    {
        /// <summary>
        /// Actualiza un usuario si se encuentra en la base de datos.
        /// </summary>
        /// <param name="model">El modelo del usuario que se desea insertar.</param>
        /// <param name="userId">El id del usuario que se desea actualizar.</param>
        /// <param name="userIdAudit">El id del usuario que esta haciendo la actualizacion.</param>
        /// <returns>
        /// Retorna -1 si el email ya esta registrado.
        /// Retorna 1 si el usuario no existe.
        /// Retorna 0 si todo se realizo con exito.
        /// </returns>
        Task<int> Execute(UpdateUserDTO model, int userId, int userIdAudit);
    }
}
