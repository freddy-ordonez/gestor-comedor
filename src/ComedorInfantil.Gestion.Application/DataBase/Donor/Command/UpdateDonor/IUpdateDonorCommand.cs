using ComedorInfantil.Gestion.Application.DTOs.Donor;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Command.UpdateDonor
{
    public interface  IUpdateDonorCommand
    {
        Task<bool> Execute(UpdateDonorDTO model, int donorId, int userId);
    }
}
