using Microsoft.AspNetCore.Mvc;

namespace ADEO.MeetingApp.Web.Controllers
{
    public class UserMessagesController() : Controller
    { 
        [HttpGet]
        public async Task<IActionResult> LatestEditor()
        { 
            return View();
        }

        public async Task<IActionResult> ClassicEditor()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ClassicEditor([FromForm] string content)
        {

            return View();
        } 
    }
}
