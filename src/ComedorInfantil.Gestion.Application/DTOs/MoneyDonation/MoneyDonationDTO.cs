namespace ComedorInfantil.Gestion.Application.DTOs.MoneyDonation
{
    public class MoneyDonationDTO
    {
        public int MoneyDonationId { get; set; }
        public int DonorId { get; set; }
        public string Porpuse { get; set; }
        public Decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
