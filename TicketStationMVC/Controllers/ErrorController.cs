using Microsoft.AspNetCore.Mvc;

namespace TicketStationMVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/500")]
        public IActionResult ServerError()
        {
            Response.StatusCode = 500; // Set status code explicitly
            return View("500"); // Returns the 500.cshtml view
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("404"); // Create a 404.cshtml if needed
                default:
                    return View("GenericError"); // Optional generic error page
            }
        }
    }
}
