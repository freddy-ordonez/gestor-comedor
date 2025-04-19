
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Command.DeleteDonor
{
    public class DeleteDonorCommand : IDeleteDonorCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteDonorCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<int> Execute(int donorId, int userId)
        {
            var donorMoneyDonationRelation = await _dataBaseService.MoneyDonations.FirstOrDefaultAsync(x => x.DonorId == donorId);
            var donorInKindDonationRelation = await _dataBaseService.InKindDonations.FirstOrDefaultAsync(x => x.DonorId == donorId);
            if ( (donorMoneyDonationRelation != null) || (donorInKindDonationRelation != null)) 
                return -1;

            var donor = await _dataBaseService.Donors.FirstOrDefaultAsync(x => x.DonorId == donorId);
            if (donor == null)
                return 1;

            _dataBaseService.Donors.Remove(donor);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino el donante con nombre de: {donor.FirstName} {donor.LastName} y id: {donorId}",
            };

            return await _createAuditCommand.Execute(newAudit) ? 0 : -1;
        }
    }
}
