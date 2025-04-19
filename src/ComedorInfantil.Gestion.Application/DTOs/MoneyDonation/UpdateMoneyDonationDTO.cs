namespace ComedorInfantil.Gestion.Application.DTOs.MoneyDonation
{
    public class UpdateMoneyDonationDTO
    {
        public string? Porpuse { get; set; }
        public Decimal? Amount { get; set; }
        public DateTime? DonationDate { get; set; }
    }
}
