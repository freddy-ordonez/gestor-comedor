using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.CreateInventory;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.DeleteInventory;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.UpdateInventory;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetAllInventoy;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetInventoryById;
using ComedorInfantil.Gestion.Application.DTOs.Inventory;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/inventories")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class InventoryController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllInventories([FromServices] IGetAllInventoyQuerie getAllInventoy)
        {
            var listDonores = await getAllInventoy.Execute();
            if (listDonores.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listDonores, "Se cargo correctamente los registros..."));
        }

        [HttpGet("{inventoryId:int}")]
        public async Task<IActionResult> GetInventoryById(int inventoryId, [FromServices] IGetInventoryByIdQuerie getInventoryById)
        {
            var donor = await getInventoryById.Execute(inventoryId);
            if (donor == null) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se encontro ningun registro en el sistema..."));

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, donor, message: "Se cargo con exito el registro..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDTO createInventoryDTO
            , [FromServices] ICreateInventoryCommand createInventory)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newInventory = await createInventory.Execute(createInventoryDTO, 
                int.Parse(userIdToken));

            return CreatedAtAction(nameof(GetInventoryById), new { inventoryId = newInventory.InventoryId }, ResponseApiService.Response(StatusCodes.Status201Created, newInventory, "Se registro el inventario con exito..."));
        }

        [HttpPut("{inventoryId:int}")]
        public async Task<IActionResult> UpdateInventory(int inventoryId, [FromBody] UpdateInventoryDTO updateInventoryDTO
            , [FromServices] IUpdateInventoryCommand updateInventory)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUpdateDonor = await updateInventory.Execute(updateInventoryDTO, inventoryId, 
                int.Parse(userIdToken));

            if (!isUpdateDonor) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se encontro ningun inventario registrado..."));

            return NoContent();
        }

        [HttpDelete("{inventoryId:int}")]
        public async Task<IActionResult> DeleteInventory(int inventoryId, [FromServices] IDeleteInventoryCommand deleteInventory)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDelete = await deleteInventory.Execute(inventoryId, int.Parse(userIdToken));

            if (isDelete == -1) return BadRequest(ResponseApiService.Response(StatusCodes.Status400BadRequest, message: "El registro de inventario no se puede eliminar ya que esta haciendo utilizada por otro registro en el sistema..."));

            if (isDelete == 1) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se pudo eliminar el inventario ya que no se encuentra en el sistema"));

            return NoContent();
        }
    }
}
