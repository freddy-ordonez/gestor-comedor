using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.CreateMoneyDonation
{
    public interface ICreateMoneyDonationCommand
    {
        Task<MoneyDonationDTO> Execute(CreateMoneyDonationDTO model, int userId);
    }
}
