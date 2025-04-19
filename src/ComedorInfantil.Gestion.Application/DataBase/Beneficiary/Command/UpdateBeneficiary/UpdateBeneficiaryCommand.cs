using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.UpdateBeneficiary
{
    public class UpdateBeneficiaryCommand : IUpdateBeneficiaryCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public UpdateBeneficiaryCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(UpdateBeneficiaryDTO model, int beneficiaryId, int userId)
        {
            var entity = await _dataBaseService.Beneficiaries.FirstOrDefaultAsync(x => x.BeneficiaryId == beneficiaryId);

            if (entity == null) 
                return false;

            _mapper.Map(model, entity);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Actualizo el beneficiario con nombre de: {model.FirstName} {model.LastName} y id: {beneficiaryId}",
            };

            return await _createAuditCommand.Execute(newAudit);

        }
    }
}
