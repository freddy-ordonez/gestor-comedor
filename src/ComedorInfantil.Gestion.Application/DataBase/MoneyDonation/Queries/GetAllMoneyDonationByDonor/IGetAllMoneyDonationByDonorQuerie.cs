using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonationByDonor
{
    public interface IGetAllMoneyDonationByDonorQuerie
    {
        Task<List<MoneyDonationDTO>> Execute(int donorId);
    }
}
