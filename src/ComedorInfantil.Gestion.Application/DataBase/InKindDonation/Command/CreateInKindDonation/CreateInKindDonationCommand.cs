using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;
using ComedorInfantil.Gestion.Domain.Entities.InKindDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.CreateInKindDonationCommand
{
    public class CreateInKindDonationCommand : ICreateInKindDonationCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateInKindDonationCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<InKindDonationDTO> Execute(CreateInKindDonationDTO model, int userId)
        {
            var newInKindDonation = _mapper.Map<InKindDonationEntity>(model);
            _dataBaseService.InKindDonations.Add(newInKindDonation);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto la donacion en especie de el donante con id de: {newInKindDonation.DonorId} y el inventario con id: {newInKindDonation.ProductId}",
            };

            await _createAuditCommand.Execute(newAudit);

            return _mapper.Map<InKindDonationDTO>(newInKindDonation);
        }
    }
}
