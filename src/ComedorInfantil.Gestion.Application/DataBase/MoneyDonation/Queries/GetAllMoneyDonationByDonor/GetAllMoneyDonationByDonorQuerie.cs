using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonationByDonor
{
    public class GetAllMoneyDonationByDonorQuerie : IGetAllMoneyDonationByDonorQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllMoneyDonationByDonorQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<MoneyDonationDTO>> Execute(int donorId)
        {
            var listMoneyDonations = await _dataBaseService.Donors.
                Include(x => x.MoneyDonations)
                .FirstOrDefaultAsync(x => x.DonorId == donorId);

            return _mapper.Map<List<MoneyDonationDTO>>(listMoneyDonations.MoneyDonations);
        }
    }
}
