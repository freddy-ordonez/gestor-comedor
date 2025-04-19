using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetAllUser
{
    public class GetAllUserQuerie : IGetAllUserQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllUserQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> Execute()
        {
            var listUser = await _dataBaseService.Users.ToListAsync();
            return _mapper.Map<List<UserDTO>>(listUser);
        }
    }
}
