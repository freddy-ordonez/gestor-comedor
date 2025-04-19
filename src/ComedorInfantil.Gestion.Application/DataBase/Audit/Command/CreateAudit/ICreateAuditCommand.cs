using ComedorInfantil.Gestion.Application.DTOs.Audit;

namespace ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit
{
    public interface ICreateAuditCommand
    {
        Task<bool> Execute(CreateAuditDTO model);
    }
}
