using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using ComedorInfantil.Gestion.Domain.Entities.MoneyDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.CreateMoneyDonation
{
    public class CreateMoneyDonationCommand : ICreateMoneyDonationCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateMoneyDonationCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<MoneyDonationDTO> Execute(CreateMoneyDonationDTO model, int userId)
        {
            var newMoneyDonation = _mapper.Map<MoneyDonationEntity>(model);
            _dataBaseService.MoneyDonations.Add(newMoneyDonation);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto la donacion monetaria con proposito de: {model.Porpuse} y id: {newMoneyDonation.MoneyDonationId}",
            };

            await _createAuditCommand.Execute(newAudit);

            return _mapper.Map<MoneyDonationDTO>(newMoneyDonation);
        }
    }
}
