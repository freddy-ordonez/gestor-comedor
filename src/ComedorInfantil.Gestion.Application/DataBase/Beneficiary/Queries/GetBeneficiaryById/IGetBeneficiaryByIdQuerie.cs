using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetBeneficiaryById
{
    public interface IGetBeneficiaryByIdQuerie
    {
        Task<BeneficiaryDTO> Execute(int beneficiaryId);
    }
}
