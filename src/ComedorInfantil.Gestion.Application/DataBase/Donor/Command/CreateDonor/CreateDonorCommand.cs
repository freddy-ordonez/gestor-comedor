using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Donor;
using ComedorInfantil.Gestion.Domain.Entities.Donor;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Command.CreateDonor
{
    public class CreateDonorCommand : ICreateDonorCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateDonorCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<DonorDTO> Execute(CreateDonorDTO model, int userId)
        {
            var newDonor = _mapper.Map<DonorEntity>(model);
            _dataBaseService.Donors.Add(newDonor);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto el donante con nombre de: {model.FirstName} {model.LastName} y id: {newDonor.DonorId}",
            };

            await _createAuditCommand.Execute(newAudit);

            return _mapper.Map<DonorDTO>(newDonor);
        }
    }
}
