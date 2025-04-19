using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.CreateMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.DeleteMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.UpdateMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonationByDonor;
using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/money-donations")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class MoneyDonationController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllMoneyDonation([FromServices] IGetAllMoneyDonationQuerie getAllMoneyDonationQuerie)
        {
            var listMoneyDonation = await getAllMoneyDonationQuerie.Execute();
            if (listMoneyDonation.Count == 0) return NoContent();
            
            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listMoneyDonation, "Se cargaron correctamente los registrado..."));
        }

        [HttpGet("donors/{donorId:int}")]
        public async Task<IActionResult> GetAllMoneyDonationsForOnoDonor(int donorId
            , [FromServices] IGetAllMoneyDonationByDonorQuerie getAllMoneyDonationByDonor)
        {
            var listMoneyDonations = await getAllMoneyDonationByDonor.Execute(donorId);
            if (listMoneyDonations.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listMoneyDonations, "Se cargaron correctamente los registrado..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoneyDonation(int donorId, [FromBody] CreateMoneyDonationDTO createMoneyDonationDTO
            , [FromServices] ICreateMoneyDonationCommand createMoneyDonationCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newMoneyDonation = await createMoneyDonationCommand.Execute(createMoneyDonationDTO, 
                int.Parse(userIdToken));

            return CreatedAtAction(nameof(GetAllMoneyDonationsForOnoDonor), new { donorId = newMoneyDonation.DonorId }, ResponseApiService.Response(StatusCodes.Status201Created, newMoneyDonation, "Se registro con exito la donacion..."));
        }

        [HttpPut("{moneyDonationId:int}")]
        public async Task<IActionResult> UpdateMoneyDonation(int moneyDonationId,
            [FromBody] UpdateMoneyDonationDTO updateMoneyDonationDTO,
            [FromServices] IUpdateMoneyDonationCommand updateMoneyDonationCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUpdateMoneyDonation = await updateMoneyDonationCommand.Execute(updateMoneyDonationDTO,
                moneyDonationId, int.Parse(userIdToken));

            if (!isUpdateMoneyDonation) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo actualizar el registro ya que no se encuentra en el sistema..."));

            return NoContent();
        }

        [HttpDelete("{moneyDonationId:int}")]
        public async Task<IActionResult> DeleteMoneyDonation(int moneyDonationId,
            [FromServices] IDeleteMoneyDonationCommand deleteMoneyDonationCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteMoneyDonation = await deleteMoneyDonationCommand.Execute(moneyDonationId,
                int.Parse(userIdToken));

            if(!isDeleteMoneyDonation) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo eliminar el registro ya que no se encuentra en el sistema..."));

            return NoContent();
        }
    }
}
