using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;
using ComedorInfantil.Gestion.Domain.Entities.Beneficiary;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.CreateBeneficiary
{
    public class CreateBeneficiaryCommand : ICreateBeneficiaryCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateBeneficiaryCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<BeneficiaryDTO> Execute(CreateBeneficiaryDTO model, int userId)
        {
            var newBeneficiary = _mapper.Map<BeneficiaryEntity>(model);
            _dataBaseService.Beneficiaries.Add(newBeneficiary);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto el beneficiario con nombre de: {model.FirstName} {model.LastName} y id: {newBeneficiary.BeneficiaryId}",
            };

            await _createAuditCommand.Execute(newAudit);
            return _mapper.Map<BeneficiaryDTO>(newBeneficiary);
        }
    }
}
