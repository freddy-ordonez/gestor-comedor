using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Activity;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetAllActivity
{
    public class GetAllActivityQuerie : IGetAllActivityQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllActivityQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<ActivityDTO>> Exexute()
        {
            var activities = await _dataBaseService.Activities.ToListAsync();
            return _mapper.Map<List<ActivityDTO>>(activities);  
        }
    }
}
