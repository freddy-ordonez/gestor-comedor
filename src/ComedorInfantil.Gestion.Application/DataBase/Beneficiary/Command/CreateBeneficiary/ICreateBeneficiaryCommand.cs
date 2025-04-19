using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.CreateBeneficiary
{
    public interface ICreateBeneficiaryCommand
    {
        Task<BeneficiaryDTO> Execute(CreateBeneficiaryDTO model, int userId);
    }
}
