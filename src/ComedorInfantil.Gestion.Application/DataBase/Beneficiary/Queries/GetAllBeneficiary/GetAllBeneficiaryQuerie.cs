using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetAllBeneficiary
{
    public class GetAllBeneficiaryQuerie : IGetAllBeneficiaryQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllBeneficiaryQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<BeneficiaryDTO>> Execute()
        {
            var beneficiaries = await _dataBaseService.Beneficiaries.ToListAsync();

            return _mapper.Map<List<BeneficiaryDTO>>(beneficiaries);
        }
    }
}
