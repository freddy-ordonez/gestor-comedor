namespace ComedorInfantil.Gestion.Application.DTOs.Beneficiary
{
    public class CreateBeneficiaryDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Status { get; set; }
    }
}
