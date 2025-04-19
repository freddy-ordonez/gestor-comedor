using ComedorInfantil.Gestion.Application.DTOs.Volunteer;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetAllVolunteer
{
    public interface IGetAllVolunteerQuerie
    {
        /// <summary>
        /// Este metodo obtiene todo los registros de los voluntario que esten activos en el sistema.
        /// </summary>
        /// <returns><Lista de objetos <see cref="VolunteerDTO">/returns>
        Task<List<VolunteerDTO>> Execute();
    }
}
