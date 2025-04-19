using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Domain.Entities.AssignmentActitvity;

namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.CreateAssigmentActivity
{
    public class CreateAssigmentActivityCommand : ICreateAssigmentActivityCommand
    {

        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateAssigmentActivityCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<AssigmentActivityDTO> Execute(CreateAssigmentActivityDTO model, int userId)
        {
            var entity = _mapper.Map<AssignmentActitvityEntity>(model);
            _dataBaseService.AssignmentActitvities.Add(entity);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto la asignacion de actividad id: {entity.AssignmentId} y id de la actividad: {entity.ActivityId}",
            };

            await _createAuditCommand.Execute(newAudit);

            return _mapper.Map<AssigmentActivityDTO>(entity);
        }
    }
}
