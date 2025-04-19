using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Volunteer;
using ComedorInfantil.Gestion.Domain.Entities.Volunteer;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.CreateVolunteer
{
    public class CreateVolunteerCommand : ICreateVolunteerCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateVolunteerCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<VolunteerDTO> Execute(CreateVolunteerDTO model, int userId)
        {
            var isIdentificationExist = await _dataBaseService.Volunteers
                .Where(x => x.Identification == model.Identification)
                .FirstOrDefaultAsync();
            if (isIdentificationExist != null)
                return null;

            var newVolunteer = _mapper.Map<VolunteerEntity>(model);
            _dataBaseService.Volunteers.Add(newVolunteer);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto el voluntario con nombre de: {model.FirstName} {model.LastName} y id: {newVolunteer.VolunteerId}",
            };

            await _createAuditCommand.Execute(newAudit);
            return _mapper.Map<VolunteerDTO>(newVolunteer);

        }
    }
}
