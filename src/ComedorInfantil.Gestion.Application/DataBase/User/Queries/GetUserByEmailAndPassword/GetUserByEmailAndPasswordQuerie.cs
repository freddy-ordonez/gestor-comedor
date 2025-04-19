using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.Features.User;
using ComedorInfantil.Gestion.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserByEmailAndPassword
{
    public class GetUserByEmailAndPasswordQuerie : IGetUserByEmailAndPasswordQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        private IPasswordService _passwordService;
        public GetUserByEmailAndPasswordQuerie(IDataBaseService dataBaseService, IMapper mapper, IPasswordService passwordService)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<AuthUserLogin> Execute(LoginUserDTO loginUserDTO)
        {
            var user = await _dataBaseService.Users
                .FirstOrDefaultAsync(x => (x.Email == loginUserDTO.Email) && (x.Status == "Activo"));

            if (user is null) return null;

            var isPasswordValid = _passwordService.VerifyPassword(user.Password, loginUserDTO.Password, user);
            if (!isPasswordValid) return null;

            return _mapper.Map<AuthUserLogin>(user);
        }
    }
}
