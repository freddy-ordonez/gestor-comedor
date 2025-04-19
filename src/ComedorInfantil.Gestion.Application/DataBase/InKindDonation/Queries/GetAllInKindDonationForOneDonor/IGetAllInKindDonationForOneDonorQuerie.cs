using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetInKindDonationForOneDonor
{
    public interface IGetAllInKindDonationForOneDonorQuerie
    {
        Task<List<InKindDonationForDonorDTO>> Execute(int donorId);
    }
}
