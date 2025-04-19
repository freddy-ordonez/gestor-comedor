using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetInKindDonationForOneInventary
{
    public interface IGetInKindDonationForOneInventaryQuerie
    {
        Task<InKindDonationForInventoryDTO> Execute(int inventoryId);
    }
}
