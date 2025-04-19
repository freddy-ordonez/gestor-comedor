namespace ComedorInfantil.Gestion.Application.DTOs.InKindDonation
{
    public class CreateInKindDonationDTO
    {
        public int? DonorId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? DonationDate { get; set; }
    }
}
