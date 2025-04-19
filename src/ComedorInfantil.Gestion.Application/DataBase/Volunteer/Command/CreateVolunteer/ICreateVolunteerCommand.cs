using ComedorInfantil.Gestion.Application.DTOs.Volunteer;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.CreateVolunteer
{
    public interface ICreateVolunteerCommand
    {
        Task<VolunteerDTO> Execute(CreateVolunteerDTO model, int userId);
    }
}
