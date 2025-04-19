using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Activity;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetActivityByIdQuerie
{
    public class GetActivityByIdQuerie : IGetActivityByIdQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetActivityByIdQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<ActivityDTO> Exexute(int activityId)
        {
            var findActivity = await _dataBaseService.Activities.
                FirstOrDefaultAsync(x => x.ActivityId == activityId);

            return _mapper.Map<ActivityDTO>(findActivity);

        }
    }
}
