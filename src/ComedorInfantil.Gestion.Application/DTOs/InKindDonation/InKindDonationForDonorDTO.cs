namespace ComedorInfantil.Gestion.Application.DTOs.InKindDonation
{
    public class InKindDonationForDonorDTO
    {
        public int InKindDonationId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
