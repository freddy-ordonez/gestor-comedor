namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.DeleteVolunteer
{
    public interface IDeleteVolunteerCommand
    {
        /// <summary>
        /// Elimina de forma logica(estatus:Inactivo) un voluntario si se encuentra en la base de datos.
        /// </summary>
        /// <param name="volunteerId">El id del voluntario que se desea eliminar de manera logica.</param>
        /// <param name="userId">El id del usuario que esta haciendo la eliminacion.</param>
        /// <returns>
        /// Retorna false si el voluntario no existe.
        /// Retorna true si todo se realizo con exito.
        /// </returns>
        Task<bool> Execute(int volunteerId, int userId);
    }
}
