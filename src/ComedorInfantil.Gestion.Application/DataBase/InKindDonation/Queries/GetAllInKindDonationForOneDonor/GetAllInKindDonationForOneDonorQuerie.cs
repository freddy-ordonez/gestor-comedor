using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetInKindDonationForOneDonor
{
    public class GetAllInKindDonationForOneDonorQuerie : IGetAllInKindDonationForOneDonorQuerie
    {
        private readonly IDataBaseService _dataBaseService;

        public GetAllInKindDonationForOneDonorQuerie(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;            
        }

        public async Task<List<InKindDonationForDonorDTO>> Execute(int donorId)
        {
            var listInKindDonationForDonor = await (from inKindDonation in _dataBaseService.InKindDonations
                                                    join inventory in _dataBaseService.Inventories
                                                    on inKindDonation.ProductId equals inventory.InventoryId
                                                    where inKindDonation.DonorId == donorId
                                                    select new InKindDonationForDonorDTO
                                                    {
                                                        DonationDate = inKindDonation.DonationDate,
                                                        ProductName = inventory.ProductName,
                                                        InKindDonationId = inKindDonation.InKindDonationId,
                                                        ProductQuantity = inventory.Quantity,
                                                    }
                                                    ).ToListAsync();

            return listInKindDonationForDonor;
        }
    }
}
