using ComedorInfantil.Gestion.Application.DataBase.TypeIdentification.Queries.GetAllTypeIdentification;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/type-identifications")]
    [ApiController]
    public class TypeIdentificationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTypeIdentification([FromServices] IGetAllTypeIdentificationQuerie getAllTypeIdentification)
        {
            var listTypeIdentification = await getAllTypeIdentification.Execute();
            if (listTypeIdentification.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listTypeIdentification, "Se cargo correctamente los registros..."));
        }
    }
}
