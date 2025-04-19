using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Donor;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Command.UpdateDonor
{
    public class UpdateDonorCommand : IUpdateDonorCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public UpdateDonorCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(UpdateDonorDTO model, int donorId, int userId)
        {
            var updateDonor = await _dataBaseService.Donors.FirstOrDefaultAsync(x => x.DonorId == donorId);

            if (updateDonor == null)
                return false;

            _mapper.Map(model, updateDonor);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Inserto el beneficiario con nombre de: {model.FirstName} {model.LastName} y id: {donorId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
