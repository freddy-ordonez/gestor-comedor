using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonation
{
    public class GetAllMoneyDonationQuerie : IGetAllMoneyDonationQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllMoneyDonationQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<MoneyDonationDTO>> Execute()
        {
            var listMoneyDonation = await _dataBaseService.MoneyDonations.ToListAsync();

            return _mapper.Map<List<MoneyDonationDTO>>(listMoneyDonation);
        }
    }
}
