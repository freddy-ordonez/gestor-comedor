using ComedorInfantil.Gestion.Application.DTOs.Volunteer;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.UpdateVolunteer
{
    public interface IUpdateVolunteerCommand
    {
        /// <summary>
        /// Actualiza un voluntario si se encuentra en la base de datos.
        /// </summary>
        /// <param name="model">El modelo del voluntario que se desea insertar.</param>
        /// <param name="volunteerId">El id del voluntario que se desea actualizar.</param>
        /// <param name="userId">El id del usuario que esta haciendo la actualizacion.</param>
        /// <returns>
        /// Retorna -1 si la identificacion ya esta registrado.
        /// Retorna 1 si el voluntario no existe.
        /// Retorna 0 si todo se realizo con exito.
        /// </returns>
        Task<int> Execute(UpdateVolunteerDTO model, int volunteerId, int userId); 
    }
}
