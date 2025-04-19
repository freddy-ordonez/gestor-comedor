using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.DeleteMoneyDonation
{
    public class DeleteMoneyDonationCommand : IDeleteMoneyDonationCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteMoneyDonationCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(int moneyDonationId, int userId)
        {
            var deleteMoneyDonation = await _dataBaseService.MoneyDonations
                .FirstOrDefaultAsync(x => x.MoneyDonationId == moneyDonationId);
            if (deleteMoneyDonation is null) return false;

            _dataBaseService.MoneyDonations.Remove(deleteMoneyDonation);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino la donacion monetaria con proposito de: {deleteMoneyDonation.Porpuse} y id: {moneyDonationId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
