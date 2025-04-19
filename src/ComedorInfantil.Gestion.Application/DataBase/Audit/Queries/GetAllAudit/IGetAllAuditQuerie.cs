using ComedorInfantil.Gestion.Application.DTOs.Audit;

namespace ComedorInfantil.Gestion.Application.DataBase.Audit.Queries.GetAllAudit
{
    public interface IGetAllAuditQuerie
    {
        /// <summary>
        /// Este metodo trae todos los registros de auditoria de el sistema.
        /// </summary>
        /// <returns>Nos devulve una lista de <see cref="AuditDTO"/></returns>
        Task<List<AuditDTO>> Execute();
    }
}
