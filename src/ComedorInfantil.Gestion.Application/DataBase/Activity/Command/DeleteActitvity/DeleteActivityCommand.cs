using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Command.DeleteActitvity
{
    public class DeleteActivityCommand : IDeleteActivityCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteActivityCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<int> Execute(int activityId, int userId)
        {

            var isActivityHadRelation = await _dataBaseService.AssignmentActitvities
                .Where(x => x.ActivityId == activityId)
                .FirstOrDefaultAsync();
            if (isActivityHadRelation != null) return -1;
                    
            var entityRemove = await _dataBaseService.Activities.FirstOrDefaultAsync(x => x.ActivityId == activityId);
            if (entityRemove == null)
                return 1;

            _dataBaseService.Activities.Remove(entityRemove);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino la actividad con nombre de: {entityRemove.Name} y id: {activityId}",
            };

            await _createAuditCommand.Execute(newAudit);
            return 0;
        }
    }
}
