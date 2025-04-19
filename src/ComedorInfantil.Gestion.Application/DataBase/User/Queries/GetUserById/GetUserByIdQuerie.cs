using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserById
{
    public class GetUserByIdQuerie : IGetUserByIdQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetUserByIdQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;   
        }

        public async Task<UserDTO> Execute(int userId)
        {
            var user = await _dataBaseService.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
