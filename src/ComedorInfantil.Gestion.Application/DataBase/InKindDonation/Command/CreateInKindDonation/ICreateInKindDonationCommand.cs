using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.CreateInKindDonationCommand
{
    public interface ICreateInKindDonationCommand
    {
        Task<InKindDonationDTO> Execute(CreateInKindDonationDTO model, int userId);
    }
}
