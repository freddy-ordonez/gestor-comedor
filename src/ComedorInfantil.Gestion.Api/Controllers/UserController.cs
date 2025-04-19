using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.CreateUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.DeleteUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdatePasswordUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdateUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetAllUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserById;
using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Authorize]
    [Route("api/v1/users")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromServices] IGetAllUserQuerie getAllUserQuerie)
        {
            var listUsers = await getAllUserQuerie.Execute();
            if (listUsers.Count == 0) return NoContent();

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, listUsers, "Se a cargado correctamente los registros..."));
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId,
            [FromServices] IGetUserByIdQuerie getUserByIdQuerie)
        {
            var user = await getUserByIdQuerie.Execute(userId);
            if (user is null) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "No se encontro ningun usuario registrado..."));

            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, user, "Se encontro el usuario registrado con exito..."));
        }

        [HttpPost("password/{userId}")]
        public async Task<IActionResult> UpdatePasswordUser(int userId, [FromBody] UpdatePasswordUserDTO updatePasswordUserDTO
            , [FromServices] IUpdatePasswordUserCommand updatePasswordUserCommand) 
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUpdate = await updatePasswordUserCommand.Execute(updatePasswordUserDTO, userId, int.Parse(userIdToken));
            if (!isUpdate) return BadRequest(ResponseApiService.Response(StatusCodes.Status400BadRequest, message: "El registro de el usuario no se pudo actualizar hable con el administrador"));
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO,
            [FromServices] ICreateUserCommand createUserCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newUser = await createUserCommand.Execute(createUserDTO, int.Parse(userIdToken));
            if (newUser is null) return BadRequest(ResponseApiService.Response(StatusCodes.Status400BadRequest, message: "El registro de el usuario no se pudo completar ya que hay un registro con ese correo utilizado por otro registro en el sistema..."));

            return CreatedAtAction(nameof(GetUserById), new { userId = newUser.UserId },
                ResponseApiService.Response(StatusCodes.Status201Created, newUser, "Se registro el usuario con exito..."));
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDTO updateUserDTO,
            [FromServices] IUpdateUserCommand updateUserCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userUpdate = await updateUserCommand.Execute(updateUserDTO, userId, int.Parse(userIdToken));
            if (userUpdate == -1) return BadRequest(ResponseApiService.Response(StatusCodes.Status400BadRequest, message: "El registro de el usuario no se puede actualizar ya que hay un registro con ese email utilizado por otro registro en el sistema..."));

            if (userUpdate == 1) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "El registro de el usuario no se puede actualizar ya que no se encuentra en el sistema..."));

            return NoContent();
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId,
            [FromServices] IDeleteUserCommand deleteUserCommand)
        {
            var userIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isDeleteUser = await deleteUserCommand.Execute(userId, int.Parse(userIdToken));
            if (!isDeleteUser) return NotFound(ResponseApiService.Response(StatusCodes.Status404NotFound, message: "El registro de el usuario no se puede eleminar ya que no se encuentra en el sistema..."));

            return NoContent();
        }
    }
}
