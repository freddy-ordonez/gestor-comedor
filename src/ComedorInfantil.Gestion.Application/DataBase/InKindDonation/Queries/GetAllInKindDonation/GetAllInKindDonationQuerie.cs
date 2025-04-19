using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetAllInKindDonation
{
    public class GetAllInKindDonationQuerie : IGetAllInKindDonationQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllInKindDonationQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<InKindDonationDTO>> Execute()
        {
            var listInKindDonatios = await _dataBaseService.InKindDonations.ToListAsync();
            return _mapper.Map<List<InKindDonationDTO>>(listInKindDonatios);
        }
    }
}
