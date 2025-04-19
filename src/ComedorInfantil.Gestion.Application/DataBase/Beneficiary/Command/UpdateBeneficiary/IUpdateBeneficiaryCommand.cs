using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;

namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.UpdateBeneficiary
{
    public interface IUpdateBeneficiaryCommand
    {
        Task<bool> Execute(UpdateBeneficiaryDTO model, int beneficiaryId, int userId);
    }
}
