namespace ComedorInfantil.Gestion.Application.DTOs.MoneyDonation
{
    public class CreateMoneyDonationDTO
    {
        public int? DonorId { get; set; }
        public string? Porpuse { get; set; }
        public Decimal? Amount { get; set; }
        public DateTime? DonationDate { get; set; }
    }
}
