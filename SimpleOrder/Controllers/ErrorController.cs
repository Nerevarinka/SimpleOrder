using Microsoft.AspNetCore.Mvc;

namespace SimpleOrder.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
