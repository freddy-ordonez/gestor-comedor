using ComedorInfantil.Gestion.Application.DTOs.Donor;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Command.CreateDonor
{
    public interface ICreateDonorCommand
    {
        Task<DonorDTO> Execute(CreateDonorDTO model, int userId);
    }
}
