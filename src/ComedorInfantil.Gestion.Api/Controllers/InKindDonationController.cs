using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.CreateInKindDonationCommand;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.DeleteInKindDonation;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetAllInKindDonation;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetInKindDonationForOneDonor;
using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/in-kind-donations")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class InKindDonationController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllInKindDonations([FromServices] IGetAllInKindDonationQuerie getAllInKindDonationQuerie)
        {
            var listInKindDonations = await getAllInKindDonationQuerie.Execute();
            if (listInKindDonations.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listInKindDonations, "Se cargaron correctamente los registrado..."));
        }

        [HttpGet("donors/{donorId:int}")]
        public async Task<IActionResult> GetAllInKindDonationsForOneDonor(int donorId
            , [FromServices] IGetAllInKindDonationForOneDonorQuerie getAllInKindDonationForOneDonor)
        {
            var listInKindDonations = await getAllInKindDonationForOneDonor.Execute(donorId);
            if (listInKindDonations.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listInKindDonations, "Se cargaron correctamente los registrado..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateInKindDonationForDonor(int donorId, [FromBody] CreateInKindDonationDTO inKindDonationDTO
            , [FromServices] ICreateInKindDonationCommand createInKindDonation)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newInKindDonation = await createInKindDonation.Execute(inKindDonationDTO, 
                int.Parse(userIdToken));

            return CreatedAtAction(nameof(GetAllInKindDonationsForOneDonor), new { donorId }, ResponseApiService.Response(StatusCodes.Status201Created, newInKindDonation, "Se registro con exito la donacion..."));
        }

        [HttpDelete("{inKindDonationId:int}")]
        public async Task<IActionResult> DeleteInKindDonation(int inKindDonationId,
            [FromServices] IDeleteInKindDonationCommand deleteInKindDonation)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteInKindDonation = await deleteInKindDonation.Execute(inKindDonationId,
                int.Parse(userIdToken));

            if (!isDeleteInKindDonation) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo eliminar la donacion ya que no se encuentra en el sistema"));

            return NoContent();
        }
    }
}
