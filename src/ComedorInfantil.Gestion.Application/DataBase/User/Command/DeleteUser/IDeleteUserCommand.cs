namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.DeleteUser
{
    public interface IDeleteUserCommand
    {
        /// <summary>
        /// Elimina de forma logica(poner en Inactivo) un usuario si se encuentra en la base de datos.
        /// </summary>
        /// <param name="userId">El id del usuario que se desea eliminar.</param>
        /// <param name="userIdAudit">El id del usuario que esta haciendo la eliminacion.</param>
        /// <returns>
        /// Retorna true si el usuario fue puesto en estado Inactivo.
        /// Retorna false si el usuario no existe.
        /// </returns>
        Task<bool> Execute(int userId, int userIdAudit);
    }
}
