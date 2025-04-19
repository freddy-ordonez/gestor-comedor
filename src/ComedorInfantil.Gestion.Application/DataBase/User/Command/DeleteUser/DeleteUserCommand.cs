
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.DeleteUser
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ICreateAuditCommand _createAuditCommand;

        public DeleteUserCommand(IDataBaseService dataBaseService, ICreateAuditCommand createAuditCommand)
        {
            _dataBaseService = dataBaseService;
            _createAuditCommand = createAuditCommand;
        }

        public async Task<bool> Execute(int userId, int userIdAudit)
        {
            var user = await _dataBaseService.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return false;

            user.Status = "Inactivo";
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "E",
                ActionDate = DateTime.Now,
                Description = $"Elimina de forma logica un usuario con nombre de: {user.FirstName} {user.LastName} y id: {userId}",
            };

            return await _createAuditCommand.Execute(newAudit);
        }
    }
}
