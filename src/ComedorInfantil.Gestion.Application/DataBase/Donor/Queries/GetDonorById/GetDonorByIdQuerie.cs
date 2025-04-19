using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Donor;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetDonorById
{
    public class GetDonorByIdQuerie : IGetDonorByIdQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetDonorByIdQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<DonorDTO> Execute(int donorId)
        {
            var donor = await _dataBaseService.Donors.FirstOrDefaultAsync(x => x.DonorId == donorId);

            return _mapper.Map<DonorDTO>(donor);
        }
    }
}
