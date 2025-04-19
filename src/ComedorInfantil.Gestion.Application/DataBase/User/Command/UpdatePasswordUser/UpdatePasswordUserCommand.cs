
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.Interfaces;
using ComedorInfantil.Gestion.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdatePasswordUser
{
    public class UpdatePasswordUserCommand : IUpdatePasswordUserCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;
        private readonly IPasswordService _passwordService;

        public UpdatePasswordUserCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand
            ,IPasswordService passwordService)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
            _passwordService = passwordService;
        }

        public async Task<bool> Execute(UpdatePasswordUserDTO model, int userId, int userIdAudit)
        {
            var user = await _dataBaseService.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user is null) return false;

            user.Password = _passwordService.HashPassword(model.Password, user);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userIdAudit,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Actualizo el usuario con nombre de: {user.FirstName} {user.LastName} y id: {userId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
