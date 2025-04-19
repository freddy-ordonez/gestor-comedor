using ComedorInfantil.Gestion.Application.DTOs.Activity;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetActivityByIdQuerie
{
    public interface IGetActivityByIdQuerie
    {
        Task<ActivityDTO> Exexute(int activityId);
    }
}
