using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.CreateAssigmentActivity;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.DeleteAssignmentActivity;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForActivity;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForVolunteer;
using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/assignment-activities")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class AssignmentActivityController : ControllerBase
    {

        [HttpGet("activities/{activityId:int}")]
        public async Task<IActionResult> GetAllAssignmentForOneActivity(int activityId,
            [FromServices] IGetAssignmentForActivityQuerie assignmentForActivityQuerie)
        {
            var listAssignmentsForOneActivity = await assignmentForActivityQuerie.Execute(activityId);

            if (listAssignmentsForOneActivity.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listAssignmentsForOneActivity, "Se a cargaron correctamente los registros..."));
        }

        [HttpGet("volunteers/{volunteerId:int}")]
        public async Task<IActionResult> GetAllAssigmentForOneVolunteer(int volunteerId, [FromServices] IGetAssignmentForVolunteerQuerie getAssignmentForVolunteer)
        {
            var listAssignmentsForOneVolunteer = await getAssignmentForVolunteer.Execute(volunteerId);

            if (listAssignmentsForOneVolunteer.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listAssignmentsForOneVolunteer, "Se a cargaron correctamente los registros..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssigmentActivity([FromBody] CreateAssigmentActivityDTO createAssigmentActivityDTO,
            [FromServices] ICreateAssigmentActivityCommand createAssigmentActivity)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newAssigmentActivity = await createAssigmentActivity.Execute(createAssigmentActivityDTO,
                int.Parse(userIdToken));
            return CreatedAtAction(nameof(GetAllAssignmentForOneActivity), new { activityId = newAssigmentActivity.ActivityId }, ResponseApiService.Response(StatusCodes.Status201Created, newAssigmentActivity, "Se registro la asignacion de la actividad con exito..."));
        }

        [HttpDelete("{assignmentId:int}")]
        public async Task<IActionResult> DeleteAssignmentActivity(int assignmentId, [FromServices] IDeleteAssignmentActivityCommand deleteAssignmentActivity)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteAssignment = await deleteAssignmentActivity.Execute(assignmentId, int.Parse(userIdToken));
            if (isDeleteAssignment) return NoContent();
            return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo eliminar la asignacion de la actividad ya que no se encuentra en el sistema"));
        }
    }
}
