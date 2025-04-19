using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.UpdateMoneyDonation
{
    public class UpdateMoneyDonationCommand : IUpdateMoneyDonationCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public UpdateMoneyDonationCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(UpdateMoneyDonationDTO model, int moneyDonationId, int userId)
        {
            var moneyDonation = await _dataBaseService.MoneyDonations
                .FirstOrDefaultAsync(x => x.MoneyDonationId == moneyDonationId);
            if (moneyDonation == null)
                return false;

            _mapper.Map(model, moneyDonation);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Actualizo la donacion monetaria con proposito de: {model.Porpuse} y id: {moneyDonationId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
