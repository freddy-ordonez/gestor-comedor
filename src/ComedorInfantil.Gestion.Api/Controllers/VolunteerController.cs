using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.CreateVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.DeleteVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.UpdateVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetAllVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetVolunteerById;
using ComedorInfantil.Gestion.Application.DTOs.Volunteer;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/volunteers")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class VolunteerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllVolunteers([FromServices] IGetAllVolunteerQuerie getAllVolunteerQuerie)
        {
            var listVolunteers = await getAllVolunteerQuerie.Execute();
            if (listVolunteers.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listVolunteers, "Se cargo correctamente los registros..."));
        }

        [HttpGet("{volunteerId:int}")]
        public async Task<IActionResult> GetVolunteerById(int volunteerId,
            [FromServices] IGetVolunteerByIdQuerie getVolunteerByIdQuerie)
        {
            var volunteer = await getVolunteerByIdQuerie.Execute(volunteerId);
            if (volunteer is null) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se encontro ningun voluntario registrado..."));

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, volunteer, message: "Se cargo con exito el voluntario registrado..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVolunteer([FromBody] CreateVolunteerDTO volunteerDTO,
            [FromServices] ICreateVolunteerCommand createVolunteerCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newVolunteer = await createVolunteerCommand.Execute(volunteerDTO,
                int.Parse(userIdToken));

            return CreatedAtAction(
                nameof(GetVolunteerById),
                new { volunteerId = newVolunteer.VolunteerId },
                ResponseApiService.Response(StatusCodes.Status201Created, newVolunteer, "Se registro el voluntario con exito...")
                );
        }

        [HttpPut("{volunteerId:int}")]
        public async Task<IActionResult> UpdateVolunteer(int volunteerId,
            [FromBody] UpdateVolunteerDTO volunteerDTO,
            [FromServices] IUpdateVolunteerCommand updateVolunteerCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resultUpdate = await updateVolunteerCommand.Execute(volunteerDTO, volunteerId, 
                int.Parse(userIdToken));

            if (resultUpdate == -1) return BadRequest(ResponseApiService.Response(StatusCodes.Status400BadRequest, message: "El registro de el voluntarion no se puede actualizar ya que hay un registro con esa identificacion utilizada por otro registro en el sistema..."));

            if (resultUpdate == 1) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "El registro de el voluntarion no se puede actualizar ya que esta haciendo utilizada por otro registro en el sistema..."));

            return NoContent();
        }

        [HttpDelete("{volunteerId:int}")]
        public async Task<IActionResult> DeleteVolunteer(int volunteerId,
            [FromServices] IDeleteVolunteerCommand deleteVolunteerCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteVolunteer = await deleteVolunteerCommand.Execute(volunteerId, 
                int.Parse(userIdToken));

            if (!isDeleteVolunteer) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "El registro de el voluntarion no se puede eliminar ya que no se encuentra en el sistema..."));

            return NoContent();

        }
    }
}
