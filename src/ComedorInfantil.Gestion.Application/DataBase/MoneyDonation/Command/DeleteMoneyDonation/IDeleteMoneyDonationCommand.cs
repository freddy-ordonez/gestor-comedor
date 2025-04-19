namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.DeleteMoneyDonation
{
    public interface IDeleteMoneyDonationCommand
    {
        Task<bool> Execute(int moneyDonationId, int userId);
    }
}
