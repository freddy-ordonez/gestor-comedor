using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.Module.Queries.GetAllModule;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/modules")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class ModuleController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllModule([FromServices] IGetAllModuleQuerie getAllModule)
        {
            var listModules = await getAllModule.Exexute();
            if (listModules.Count == 0) return NoContent();
            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listModules, "Se a cargado los registros con exito..."));
        }
    }
}
