using ComedorInfantil.Gestion.Application.DTOs.Donor;

namespace ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetDonorById
{
    public interface IGetDonorByIdQuerie
    {
        Task<DonorDTO> Execute(int donorId);
    }
}
