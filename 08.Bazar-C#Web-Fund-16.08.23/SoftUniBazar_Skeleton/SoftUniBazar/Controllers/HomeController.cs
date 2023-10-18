using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using System.Diagnostics;

using SoftUniBazar.Models;

namespace SoftUniBazar.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Ad");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}