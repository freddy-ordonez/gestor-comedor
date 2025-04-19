using ComedorInfantil.Gestion.Application.DTOs.Activity;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetAllActivity
{
    public interface IGetAllActivityQuerie
    {
        Task<List<ActivityDTO>> Exexute();
    }
}
