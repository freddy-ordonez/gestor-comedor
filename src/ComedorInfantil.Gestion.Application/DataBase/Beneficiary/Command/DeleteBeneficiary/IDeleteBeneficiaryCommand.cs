namespace ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.DeleteBeneficiary
{
    public interface IDeleteBeneficiaryCommand
    {
        Task<bool> Execute(int beneficiaryId, int userId);
    }
}
