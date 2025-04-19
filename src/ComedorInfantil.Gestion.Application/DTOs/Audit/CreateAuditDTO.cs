namespace ComedorInfantil.Gestion.Application.DTOs.Audit
{
    public class CreateAuditDTO
    {
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
