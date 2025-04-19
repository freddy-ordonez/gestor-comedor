using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;

namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForVolunteer
{
    public interface IGetAssignmentForVolunteerQuerie
    {
        Task<List<AssigmentForVolunteerDTO>> Execute(int volunteerId);
    }
}
