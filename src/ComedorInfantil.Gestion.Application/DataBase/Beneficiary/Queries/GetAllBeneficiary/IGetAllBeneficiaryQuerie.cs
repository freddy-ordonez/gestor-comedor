using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetAllBeneficiary
{
    public interface IGetAllBeneficiaryQuerie
    {
        Task<List<BeneficiaryDTO>> Execute();
    }
}
