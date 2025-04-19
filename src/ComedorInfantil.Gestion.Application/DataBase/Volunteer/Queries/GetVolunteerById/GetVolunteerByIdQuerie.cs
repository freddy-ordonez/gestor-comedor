using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Volunteer;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetVolunteerById
{
    public class GetVolunteerByIdQuerie : IGetVolunteerByIdQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetVolunteerByIdQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<VolunteerDTO> Execute(int volunteerId)
        {
            var isVolunteerExist = await _dataBaseService.Volunteers
                .Where(x => x.Status == "Activo")
                .FirstOrDefaultAsync(x => x.VolunteerId == volunteerId);
            return _mapper.Map<VolunteerDTO>(isVolunteerExist);
        }
    }
}
