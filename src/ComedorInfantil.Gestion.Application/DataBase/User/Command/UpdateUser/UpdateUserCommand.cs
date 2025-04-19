using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdateUser
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;
        private readonly IPasswordService _passwordService;

        public UpdateUserCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand, IPasswordService passwordService)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
            _passwordService = passwordService;
        }

        public async Task<int> Execute(UpdateUserDTO model, int userId, int userIdAudit)
        {
            var isEmailRegistered = await _dataBaseService.Users
                .Where(x => (x.Email == model.Email) && (x.UserId != userId))
                .FirstOrDefaultAsync();
            if (isEmailRegistered != null) return -1;

            var user = await _dataBaseService.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return 1;

            _mapper.Map(model, user);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userIdAudit,
                Action = "A",
                ActionDate = DateTime.Now,
                Description = $"Actualizo el usuario con nombre de: {model.FirstName} {model.LastName} y id: {userId}",
            };

            await _createAuditCommand.Execute(newAudit);
            return 0;
        }
    }
}
