
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.DeleteInKindDonation
{
    internal class DeleteInKindDonationCommand : IDeleteInKindDonationCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteInKindDonationCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _createAuditCommand = createAuditCommand;
            _dataBaseService = dataBaseService;
        }

        public async Task<bool> Execute(int inKindDonationId, int userId)
        {
            var deleteInKindDonation = await _dataBaseService.InKindDonations.FirstOrDefaultAsync(x => x.InKindDonationId == inKindDonationId);
            if (deleteInKindDonation is null) return false;

            _dataBaseService.InKindDonations.Remove(deleteInKindDonation);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino la donacion en especie con id de: {inKindDonationId}",
            };
            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
