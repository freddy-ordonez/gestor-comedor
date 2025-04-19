using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.ModuleByUser;
using ComedorInfantil.Gestion.Domain.Entities.ModuleForUser;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.CreateModuleByUser
{
    public class CreateModuleByUserCommand : ICreateModuleByUserCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public CreateModuleByUserCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<ModuleByUserDTO> Execute(CreateModuleByUserDTO model, int userId)
        {
            var newModuleByUser = _mapper.Map<ModuleForUserEntity>(model);
            _dataBaseService.ModuleForUsers.Add(newModuleByUser);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto el modulo x usuario con el userId: {newModuleByUser.UserId} y el moduleId: {newModuleByUser.ModuleId}",
            };

            await _createAuditCommand.Execute(newAudit);

            var moduleByUser = await (from module in _dataBaseService.Modules
                                      join moduleByUsers in _dataBaseService.ModuleForUsers
                                      on module.ModuleId equals moduleByUsers.ModuleId
                                      where moduleByUsers.ModuleForUserId == newModuleByUser.ModuleForUserId
                                      select new ModuleByUserDTO {
                                        ClassCSS = module.ClassCSS,
                                        Link = module.Link,
                                        ModuleForUserId = newModuleByUser.ModuleForUserId,
                                        ModuleId = newModuleByUser.ModuleId,
                                        ModuleName = module.ModuleName,
                                        UserId = newModuleByUser.UserId
                                      }).FirstOrDefaultAsync();


            return moduleByUser;
        }
    }
}
