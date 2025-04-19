using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.UpdateMoneyDonation
{
    public interface IUpdateMoneyDonationCommand
    {
        Task<bool> Execute(UpdateMoneyDonationDTO model, int moneyDonationId, int userId);
    }
}
