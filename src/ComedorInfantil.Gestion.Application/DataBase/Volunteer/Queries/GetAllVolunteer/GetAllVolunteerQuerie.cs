using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Volunteer;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetAllVolunteer
{
    public class GetAllVolunteerQuerie : IGetAllVolunteerQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllVolunteerQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<VolunteerDTO>> Execute()
        {
            var listVolunteer = await _dataBaseService.Volunteers.
                Where(x => x.Status == "Activo")
                .ToListAsync();
            return _mapper.Map<List<VolunteerDTO>>(listVolunteer);
        }
    }
}
