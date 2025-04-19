using ComedorInfantil.Gestion.Application.DTOs.Activity;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Command.CreateActivity
{
    public interface ICreateActivityCommand
    {
        Task<ActivityDTO> Execute(CreateActivityDTO model, int userId);
    }
}
