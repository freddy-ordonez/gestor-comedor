using ComedorInfantil.Gestion.Application.DTOs.Donor;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetAllDonor
{
    public interface IGetAllDonorQuerie
    {
        Task<List<DonorDTO>> Execute();
    }
}
