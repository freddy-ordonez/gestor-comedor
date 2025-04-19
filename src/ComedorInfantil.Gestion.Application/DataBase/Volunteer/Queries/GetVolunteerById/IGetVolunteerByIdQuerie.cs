using ComedorInfantil.Gestion.Application.DTOs.Volunteer;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetVolunteerById
{
    public interface IGetVolunteerByIdQuerie
    {
        /// <summary>
        /// Este metodo obtiene un registro de un voluntario si existe en el sistema.
        /// </summary>
        /// <param name="volunteerId">Id del voluntario a obtener</param>
        /// <returns>Un objeto <see cref="VolunteerDTO"/> o null si el voluntario no se encuentra en el sistema.</returns>
        Task<VolunteerDTO> Execute(int volunteerId);
    }
}
