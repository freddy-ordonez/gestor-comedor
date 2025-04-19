using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonation
{
    public interface IGetAllMoneyDonationQuerie
    {
        Task<List<MoneyDonationDTO>> Execute();
    }
}
