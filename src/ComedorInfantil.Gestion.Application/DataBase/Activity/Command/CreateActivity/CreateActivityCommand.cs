using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Activity;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Domain.Entities.Activity;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Command.CreateActivity
{
    public class CreateActivityCommand : ICreateActivityCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateActivityCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<ActivityDTO> Execute(CreateActivityDTO model, int userId)
        {
            var newActivity = _mapper.Map<ActivityEntity>(model);
            await _dataBaseService.Activities.AddAsync(newActivity);
            var isSaveActivity = await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto la actividad con nombre de: {model.Name} y id: {newActivity.ActivityId}",
            };

            await _createAuditCommand.Execute(newAudit);

            return _mapper.Map<ActivityDTO>(newActivity);

        }
    }
}
