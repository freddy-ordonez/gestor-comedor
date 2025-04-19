using Microsoft.AspNetCore.Mvc;

namespace ComedorInfantil.Gestion.UI.Controllers
{
    public class ApiController : Controller
    {
        private readonly string _apiUrl;

        public ApiController(IConfiguration config)
        {
            _apiUrl = config["ComedorApiBaseUrl"];
        }

        public IActionResult GetApiUrl()
        {
            return Ok(_apiUrl);
        }
    }
}
