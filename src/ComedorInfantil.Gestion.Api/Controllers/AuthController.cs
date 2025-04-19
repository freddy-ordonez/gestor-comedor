using ComedorInfantil.Gestion.Api.ActionFilters;
using ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserByEmailAndPassword;
using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.Features;
using ComedorInfantil.Gestion.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComedorInfantil.Gestion.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO,
            [FromServices] IGetUserByEmailAndPasswordQuerie getUserByEmailAndPasswordQuerie,
            [FromServices] IGetTokenJwtService getTokenJwtService)
        {
            var user = await getUserByEmailAndPasswordQuerie.Execute(loginUserDTO);
            if (user == null) return NotFound(ResponseApiService.Response(StatusCodes.Status200OK,message: "No se encontro ningun usuario registrado..."));

            user.Token = getTokenJwtService.Execute(user.UserId.ToString());
            return Ok(ResponseApiService.Response(StatusCodes.Status200OK, user, "Se cargo correctamente el usuario registrado..."));
        }

    }
}
