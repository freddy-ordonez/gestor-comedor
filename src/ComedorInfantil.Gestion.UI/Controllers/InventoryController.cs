using Microsoft.AspNetCore.Mvc;

namespace ComedorInfantil.Gestion.UI.Controllers
{
    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }
    }
}
