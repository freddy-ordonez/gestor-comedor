using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Donor;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetAllDonor
{
    public class GetAllDonorQuerie : IGetAllDonorQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllDonorQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<DonorDTO>> Execute()
        {
            var donors = await _dataBaseService.Donors.ToListAsync();
            return _mapper.Map<List<DonorDTO>>(donors);
        }
    }
}
