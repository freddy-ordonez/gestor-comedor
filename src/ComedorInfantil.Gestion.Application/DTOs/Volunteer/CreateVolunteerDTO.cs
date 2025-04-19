namespace ComedorInfantil.Gestion.Application.DTOs.Volunteer
{
    public class CreateVolunteerDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Identification { get; set; }
        public string? Phone { get; set; }
        public string? Availability { get; set; }
        public string? Status { get; set; }
        public int? TypeIdentification { get; set; }
    }
}
