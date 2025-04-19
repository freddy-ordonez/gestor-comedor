using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Audit.Queries.GetAllAudit
{
    public class GetAllAuditQuerie : IGetAllAuditQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllAuditQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<AuditDTO>> Execute()
        {
            var listAudit = await (from audit in _dataBaseService.Audits
                                   join user in _dataBaseService.Users
                                   on audit.UserId equals user.UserId
                                   select new AuditDTO
                                   {
                                       Action = audit.Action,
                                       AuditId = audit.AuditId,
                                       ActionDate = audit.ActionDate,
                                       Description = audit.Description,
                                       UserName = $"{user.FirstName} {user.LastName}"
                                   }).ToListAsync();

            return _mapper.Map<List<AuditDTO>>(listAudit);
        }
    }
}
