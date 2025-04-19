
using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.DeleteModuleByUser
{
    public class DeleteModuleByUserCommand : IDeleteModuleByUserCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteModuleByUserCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(int moduleByUserId, int userId)
        {
            var moduleByUser = await _dataBaseService.ModuleForUsers
                .FirstOrDefaultAsync(x => x.ModuleForUserId == moduleByUserId);
            if (moduleByUser == null)
                return false;

            _dataBaseService.ModuleForUsers.Remove(moduleByUser);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimino el modulo de el usuario con userId: {userId} y moduleId: {moduleByUser.ModuleId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
