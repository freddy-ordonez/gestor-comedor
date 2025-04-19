using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Command.CreateActivity;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Command.DeleteActitvity;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Command.UpdateActivity;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetActivityByIdQuerie;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetAllActivity;
using ComedorInfantil.Gestion.Application.DTOs.Activity;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("/api/v1/activities")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class ActivityController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllActivities([FromServices] IGetAllActivityQuerie getAllActivity)
        {
            var listActivities = await getAllActivity.Exexute();
            if (listActivities.Count <= 0) return NoContent();

            return StatusCode(StatusCodes.Status200OK,
                              ResponseApiService.Response(StatusCodes.Status200OK, listActivities, "Se a cargado correctamente los registros...")
                             );
        }

        [HttpGet("{activityId:int}")]
        public async Task<IActionResult> GetActivityById(int activityId,
            [FromServices] IGetActivityByIdQuerie getActivityById)
        {
            var activity = await getActivityById.Exexute(activityId);
            if (activity == null)
                return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, null, "No se encontro ninguna actividad registrada..."));
            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, activity, "Se encontro la actividad registrada con exito..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDTO activityDTO,
            [FromServices] ICreateActivityCommand createActivity)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newActivity = await createActivity.Execute(activityDTO, int.Parse(userIdToken));
            return CreatedAtAction(nameof(GetActivityById), new { activityId = newActivity.ActivityId }, ResponseApiService.Response(StatusCodes.Status201Created, newActivity, "Se registro la actividad con exito..."));

        }

        [HttpPut("{activityId:int}")]
        public async Task<IActionResult> UpdateActivity(int activityId, [FromBody] UpdateActivityDTO activityDTO,
            [FromServices] IUpdateActivityCommand updateActivity)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isActivityExist = await updateActivity.Execute(activityDTO, activityId, int.Parse(userIdToken));
            if (!isActivityExist)
                return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "El registro de la actividad no se encontro en el sistema..."));
            return NoContent();
        }

        [HttpDelete("{activityId:int}")]
        public async Task<IActionResult> DeleteActivity(int activityId
            , [FromServices] IDeleteActivityCommand deleteActivity)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteActivity = await deleteActivity.Execute(activityId, int.Parse(userIdToken));
            if (isDeleteActivity == -1) return BadRequest(ResponseApiService.Response(StatusCodes.Status400BadRequest, null, "El registro de la actividad no se puede eliminar ya que esta haciendo utilizada por otro registro en el sistema..."));

            if (isDeleteActivity == 1) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo eliminar la actividad ya que no se encuentra en el sistema"));

            return NoContent();
        }
    }
}
