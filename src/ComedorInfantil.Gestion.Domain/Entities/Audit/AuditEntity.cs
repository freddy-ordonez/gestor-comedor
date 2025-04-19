using ComedorInfantil.Gestion.Domain.Entities.User;

namespace ComedorInfantil.Gestion.Domain.Entities.Audit
{
    public class AuditEntity
    {
        public int AuditId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime ActionDate { get; set; }

        public UserEntity User { get; set; }
    }
}
