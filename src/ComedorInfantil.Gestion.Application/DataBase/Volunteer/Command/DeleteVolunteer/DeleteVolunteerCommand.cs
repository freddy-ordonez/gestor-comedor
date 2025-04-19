using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.DeleteVolunteer
{
    public class DeleteVolunteerCommand : IDeleteVolunteerCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteVolunteerCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(int volunteerId, int userId)
        {
            var isVolunteerExist = await _dataBaseService.Volunteers
                .FirstOrDefaultAsync(x => x.VolunteerId == volunteerId);
            if (isVolunteerExist == null) return false;

            isVolunteerExist.Status = "Inactivo";
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Elimino el voluntario con nombre de: {isVolunteerExist.FirstName} {isVolunteerExist.LastName} y id: {volunteerId}",
            };

            return await _createAuditCommand.Execute(newAudit);

        }
    }
}
