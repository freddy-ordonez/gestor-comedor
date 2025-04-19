using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Queries.GetAllAudit;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/audits")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class AuditController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAudits([FromServices] IGetAllAuditQuerie getAllAudit)
        {
            var listAudits = await getAllAudit.Execute();
            if (listAudits.Count == 0) return NoContent();
            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listAudits, "Se cargaron los registros con exito..."));
        }
    }
}
