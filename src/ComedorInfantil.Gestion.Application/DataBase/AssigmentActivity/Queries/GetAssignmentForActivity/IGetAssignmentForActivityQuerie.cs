using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;

namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForActivity
{
    public interface IGetAssignmentForActivityQuerie
    {
        Task<List<AssigmentForActivityDTO>> Execute(int activityId);
    }
}
