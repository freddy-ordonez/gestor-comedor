using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Domain.Entities.Audit;

namespace ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit
{
    public class CreateAuiditCommand : ICreateAuditCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public CreateAuiditCommand(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<bool> Execute(CreateAuditDTO model)
        {
            var newAudit = _mapper.Map<AuditEntity>(model); 
            _dataBaseService.Audits.Add(newAudit);
            return await _dataBaseService.SaveAsync();
        }
    }
}
