using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetInKindDonationForOneInventary
{
    public class GetInKindDonationForOneInventaryQuerie : IGetInKindDonationForOneInventaryQuerie
    {
        private readonly IDataBaseService _dataBaseService;

        public GetInKindDonationForOneInventaryQuerie(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task<InKindDonationForInventoryDTO> Execute(int inventoryId)
        {
            var inKindDonationForInventory = await (from inKindDonation in _dataBaseService.InKindDonations
                                                    join donor in _dataBaseService.Donors
                                                    on inKindDonation.DonorId equals donor.DonorId
                                                    where inKindDonation.ProductId == inventoryId
                                                    select new InKindDonationForInventoryDTO
                                                    {
                                                        DonationDate = inKindDonation.DonationDate,
                                                        DonorFullName = $"{donor.FirstName} {donor.LastName}",
                                                        DonorPhone = donor.Phone,
                                                        InKindDonationId = inKindDonation.InKindDonationId
                                                    }
                                                    ).FirstOrDefaultAsync();
            return inKindDonationForInventory;

        }
    }
}
