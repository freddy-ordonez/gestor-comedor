using AutoMapper;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.Interfaces;
using ComedorInfantil.Gestion.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Command.CreateUser
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private readonly ICreateAuditCommand _createAuditCommand;
        private readonly IPasswordService _passwordService;

        public CreateUserCommand(IDataBaseService dataBaseService, IMapper mapper, ICreateAuditCommand createAuditCommand, IPasswordService passwordService)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _createAuditCommand = createAuditCommand;
            _passwordService = passwordService;
        }

        public async Task<UserDTO> Execute(CreateUserDTO model, int userId)
        {
            var isEmailRegister = await _dataBaseService.Users
                .Where(x => x.Email == model.Email)
                .FirstOrDefaultAsync();

            if (isEmailRegister != null)
                return null;

            var newUser = _mapper.Map<UserEntity>(model);
            newUser.Password = _passwordService.HashPassword(newUser.Password, newUser);
            newUser.Status = "Activo";
            _dataBaseService.Users.Add(newUser);
            await _dataBaseService.SaveAsync();

            var newAudit = new CreateAuditDTO
            {
                UserId = userId,
                Action = "I",
                ActionDate = DateTime.Now,
                Description = $"Inserto el usuario con nombre de: {model.FirstName} {model.LastName} y id: {newUser.UserId}",
            };

            await _createAuditCommand.Execute(newAudit);
            return _mapper.Map<UserDTO>(newUser);
        }
    }
}
