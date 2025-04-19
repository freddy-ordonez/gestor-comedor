namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Command.DeleteDonor
{
    public interface IDeleteDonorCommand
    {
        Task<int> Execute(int donorId, int userId);
    }
}
