namespace ComedorInfantil.Gestion.Application.DTOs.InKindDonation
{
    public class InKindDonationDTO
    {
        public int InKindDonationId { get; set; }
        public int DonorId { get; set; }
        public int ProductId { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
