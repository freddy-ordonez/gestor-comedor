
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.DeleteBeneficiary
{
    public class DeleteBeneficiaryCommand : IDeleteBeneficiaryCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteBeneficiaryCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(int beneficiaryId, int userId)
        {
            var entity = await _dataBaseService.Beneficiaries.
                FirstOrDefaultAsync(x => x.BeneficiaryId == beneficiaryId);

            if (entity == null)
                return false;

            _dataBaseService.Beneficiaries.Remove(entity);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino el beneficiario con nombre de: {entity.FirstName} {entity.LastName} y id: {beneficiaryId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
