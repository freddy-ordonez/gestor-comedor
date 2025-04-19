using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Command.CreateDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Command.DeleteDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Command.UpdateDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetAllDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetDonorById;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.CreateMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonationByDonor;
using ComedorInfantil.Gestion.Application.DTOs.Donor;
using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/donors")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class DonorController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllDonors([FromServices] IGetAllDonorQuerie getAllDonor)
        {
            var listDonores = await getAllDonor.Execute();
            if (listDonores.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listDonores, "Se cargo correctamente los registros..."));
        }

        [HttpGet("{donorId:int}")]
        public async Task<IActionResult> GetDonorById(int donorId, [FromServices] IGetDonorByIdQuerie getDonorById)
        {
            var donor = await getDonorById.Execute(donorId);
            if (donor == null) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se encontro ningun donante registrado..."));

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, donor, message: "Se cargo con exito el donante registrado..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDonor([FromBody] CreateDonorDTO createDonorDTO
            , [FromServices] ICreateDonorCommand createDonorCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newDonor = await createDonorCommand.Execute(createDonorDTO,
                int.Parse(userIdToken));

            return CreatedAtAction(nameof(GetDonorById), new { donorId = newDonor.DonorId }, ResponseApiService.Response(StatusCodes.Status201Created, newDonor, "Se registro el donante con exito..."));
        }

        [HttpPut("{donorId:int}")]
        public async Task<IActionResult> UpdateDonor(int donorId, [FromBody] UpdateDonorDTO updateDonorDTO
            , [FromServices] IUpdateDonorCommand updateDonor)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUpdateDonor = await updateDonor.Execute(updateDonorDTO, donorId,
                int.Parse(userIdToken));

            if (!isUpdateDonor) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se encontro ningun donante registrado..."));

            return NoContent();
        }

        [HttpDelete("{donorId:int}")]
        public async Task<IActionResult> DeleteDonor(int donorId, [FromServices] IDeleteDonorCommand deleteDonor)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDelete = await deleteDonor.Execute(donorId, int.Parse(userIdToken));

            if (isDelete == -1) return BadRequest(ResponseApiService.Response(StatusCodes.Status400BadRequest, message: "El registro de el donante no se puede eliminar ya que esta haciendo utilizada por otro registro en el sistema..."));

            if (isDelete == 1) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo eliminar el donante ya que no se encuentra en el sistema"));

            return NoContent();
        }
    }
}
