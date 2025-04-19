using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.CreateModuleByUser;
using ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.DeleteModuleByUser;
using ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Queries.GetModuleByUserId;
using ComedorInfantil.Gestion.Application.DTOs.ModuleByUser;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/module-by-users")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class ModuleByUserController : ControllerBase
    {
        [HttpGet("users/{userId:int}")]
        public async Task<IActionResult> GetAllModuleByUser(int userId, [FromServices] IGetModuleByUserIdQuerie getModuleByUserId)
        {
            var listModules = await getModuleByUserId.Execute(userId);
            if(listModules.Count == 0) return NoContent();
            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listModules, "Se a cargado los registros con exito..."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateModuleByUser([FromBody] CreateModuleByUserDTO createModuleByUserDTO,
            [FromServices] ICreateModuleByUserCommand createModuleByUser)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newModuleByUser = await createModuleByUser.Execute(createModuleByUserDTO,
                int.Parse(userIdToken));

            return CreatedAtAction(nameof(GetAllModuleByUser), new { userId = newModuleByUser.UserId},
                ResponseApiService.Response(StatusCodes.Status201Created, newModuleByUser, "Se a agregado el nuevo modulo para el usuario con exito...."));
        }

        [HttpDelete("{moduleByUserId:int}")]
        public async Task<IActionResult> DeleteModuleByUser(int moduleByUserId, [FromServices] IDeleteModuleByUserCommand deleteModuleByUser)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteModuleByUser = await deleteModuleByUser.Execute(moduleByUserId, 
                int.Parse(userIdToken));

            if (!isDeleteModuleByUser) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message:"No se a podido eliminar el modulo de el usuario porque no se encuentra en el sistema...."));

            return NoContent();
        }
    }
}
