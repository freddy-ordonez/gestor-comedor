using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Activity;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Command.UpdateActivity
{
    public class UpdateActivityCommand : IUpdateActivityCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public UpdateActivityCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(UpdateActivityDTO model, int activityId, int userId)
        {
            var findEntity = await _dataBaseService.Activities.
                FirstOrDefaultAsync(x => x.ActivityId == activityId);

            if (findEntity == null)
                return false;

            _mapper.Map(model, findEntity);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Actualizo la actividad con nombre de: {findEntity.Name} y id: {activityId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
