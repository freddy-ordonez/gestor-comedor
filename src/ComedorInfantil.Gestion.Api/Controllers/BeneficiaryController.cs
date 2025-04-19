using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.CreateBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.DeleteBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.UpdateBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetAllBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetBeneficiaryById;
using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Route("api/v1/beneficiaries")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllBeneficiary([FromServices] IGetAllBeneficiaryQuerie getAllBeneficiary)
        {
            var listBeneficiaries = await getAllBeneficiary.Execute();
            if (listBeneficiaries.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listBeneficiaries, "Se cargo correctamente los registros..."));
        }

        [HttpGet("{beneficiaryId:int}")]
        public async Task<IActionResult> GetBeneficiaryById(int beneficiaryId, [FromServices] IGetBeneficiaryByIdQuerie getBeneficiaryById)
        {
            var beneficiary = await getBeneficiaryById.Execute(beneficiaryId);
            if (beneficiary == null) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se encontro ningun beneficiario registrado..."));

            return Ok(ResponseApiService.Response(StatusCodes.Status404NotFound, beneficiary, message: "Se cargo el beneficiario con exito..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeneficiary([FromBody] CreateBeneficiaryDTO createBeneficiaryDTO
            , [FromServices] ICreateBeneficiaryCommand createBeneficiary)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newBeneficiary = await createBeneficiary.Execute(createBeneficiaryDTO, 
                int.Parse(userIdToken));
            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, newBeneficiary, "Se registro el beneficiario con exito..."));
        }

        [HttpPut("{beneficiaryId:int}")]
        public async Task<IActionResult> UpdateBeneficiary(int beneficiaryId, [FromBody] UpdateBeneficiaryDTO updateBeneficiaryDTO
            , [FromServices] IUpdateBeneficiaryCommand updateBeneficiary)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUpdateBeneficiary = await updateBeneficiary.Execute(updateBeneficiaryDTO, 
                beneficiaryId,
                int.Parse(userIdToken));

            if (!isUpdateBeneficiary) NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "El registro de el beneficiario no se encontro en el sistema"));

            return NoContent();
        }

        [HttpDelete("{beneficiaryId:int}")]
        public async Task<IActionResult> DeleteBeneficiary(int beneficiaryId, [FromServices] IDeleteBeneficiaryCommand deleteBeneficiary)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteBeneficiary = await deleteBeneficiary.Execute(beneficiaryId,
                int.Parse(userIdToken));

            if (!isDeleteBeneficiary) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo eliminar el beneficiario ya que no se encuentra en el sistema"));

            return NoContent();
        }
    }
}
