using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetBeneficiaryById
{
    public class GetBeneficiaryByIdQuerie : IGetBeneficiaryByIdQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetBeneficiaryByIdQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<BeneficiaryDTO> Execute(int beneficiaryId)
        {
            var beneficiary = await _dataBaseService.Beneficiaries.
                FirstOrDefaultAsync(x => x.BeneficiaryId == beneficiaryId);

            return _mapper.Map<BeneficiaryDTO>(beneficiary);
        }
    }
}
