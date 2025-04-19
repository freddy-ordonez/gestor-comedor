using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;

namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.CreateAssigmentActivity
{
    public interface ICreateAssigmentActivityCommand
    {
        Task<AssigmentActivityDTO> Execute(CreateAssigmentActivityDTO model, int userId);
    }
}
