using ComedorInfantil.Gestion.Application.DTOs.Activity;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Command.UpdateActivity
{
    public interface IUpdateActivityCommand
    {
        Task<bool> Execute(UpdateActivityDTO model, int activityId, int userId);
    }
}
