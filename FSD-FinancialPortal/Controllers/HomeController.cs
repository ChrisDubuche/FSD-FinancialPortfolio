using FSD_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using FSD_FinancialPortal.Helpers;

namespace FSD_FinancialPortal.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            if  (roleHelper.IsUserInRole(userId, "Guest"))
            {
                return View("Lobby");
            }
            else
            {
            return View(user);
            }    
              


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult NotificationCount(string userId)
        {
            var count = db.Notifications.Where(n => n.SenderId == userId).Count().ToString();

            return Content(count, "string");
        }
    }
}