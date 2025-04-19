using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetAllInKindDonation
{
    public interface IGetAllInKindDonationQuerie
    {
        Task<List<InKindDonationDTO>> Execute();
    }
}
