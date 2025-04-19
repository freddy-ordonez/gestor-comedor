using Microsoft.AspNetCore.Mvc;

namespace ComedorInfantil.Gestion.UI.Controllers
{
    public class VolunteerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }
        
        public IActionResult Assignment()
        {
            return View();
        }
    }
}
