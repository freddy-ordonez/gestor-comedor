namespace ComedorInfantil.Gestion.Application.DTOs.Audit
{
    public class AuditDTO
    {
        public int AuditId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
