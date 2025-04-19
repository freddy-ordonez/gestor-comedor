using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.DeleteAssignmentActivity
{
    public class DeleteAssignmentActivityCommand : IDeleteAssignmentActivityCommand
    {
        private readonly ICreateAuditCommand _createAuditCommand;
        private readonly IDataBaseService _dataBaseService;

        public DeleteAssignmentActivityCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _createAuditCommand = createAuditCommand;
            _dataBaseService = dataBaseService;
        }

        public async Task<bool> Execute(int assignmentActivityId, int userId)
        {
            var assignmentDelete = await _dataBaseService.AssignmentActitvities.FirstOrDefaultAsync(x => x.AssignmentId == assignmentActivityId);
            if (assignmentDelete is null) return false;
            
            _dataBaseService.AssignmentActitvities.Remove(assignmentDelete);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino la asiganacion de la actividad con el id: {assignmentActivityId} y id de la actividad: {assignmentDelete.ActivityId}",
            };

            await _createAuditCommand.Execute(newAudit);
            return true;
        }
    }
}
