using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Volunteer;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.UpdateVolunteer
{
    public class UpdateVolunteerCommand : IUpdateVolunteerCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public UpdateVolunteerCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<int> Execute(UpdateVolunteerDTO model, int volunteerId, int userId)
        {
            var isIdentificationExist = await _dataBaseService.Volunteers
                .Where(x => (x.Identification == model.Identification) && (x.VolunteerId != volunteerId))
                .FirstOrDefaultAsync();
            if (isIdentificationExist != null) return -1;

            var isVolunteerExist = await _dataBaseService.Volunteers
                .FirstOrDefaultAsync(x => x.VolunteerId == volunteerId);
            if (isVolunteerExist == null) return 1;

            _mapper.Map(model, isVolunteerExist);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Actualizo el voluntario con nombre de: {model.FirstName} {model.LastName} y id: {isVolunteerExist.VolunteerId}",
            };
            await _createAuditCommand.Execute(newAudit);
            return 0;
        }
    }
}
