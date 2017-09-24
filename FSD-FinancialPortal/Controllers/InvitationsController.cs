using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FSD_FinancialPortal.Models;
using FSD_FinancialPortal.Helpers;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace FSD_FinancialPortal.Controllers
{
    [Authorize]
    public class InvitationsController : Controller
    {
        private EmailHelper emailHelper = new EmailHelper();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invitations
        [Authorize(Roles = "Admin, Head")]
        public ActionResult Index()
        {
            var invitations = db.Invitations.Include(i => i.Household);
            return View(invitations.ToList());
        }

        // GET: Invitations/Details/5
        [Authorize(Roles = "Admin, Head")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // GET: Invitations/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CreatedDate,Email,Code,HouseholdId,Expired,Accepted")] Invitation invitation)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            if (ModelState.IsValid)
            {
                invitation.HouseholdId = (int)currentUser.HouseholdId;
                invitation.CreatedDate = DateTime.Now;
                invitation.ExpirationDate = invitation.CreatedDate.AddDays(7);
                db.Invitations.Add(invitation);
                db.SaveChanges();

                var callbackUrl = Url.Action("Join", "Invitations", new { id = invitation.Id }, protocol: Request.Url.Scheme);
                var message = new EmailMessage()
                {
                    SourceName = "FSD Financial Portal",
                    SourceId = userId,
                    DestinationEmail = invitation.Email,
                    Subject = String.Concat(currentUser.FullName, " has invited you to join ", db.Households.FirstOrDefault(i => i.Id == invitation.HouseholdId).Name),
                    Body = String.Concat("Enter this code ", "<strong>", invitation.Code, "</strong> ", "<a href=\"" + callbackUrl + "\">here</a>")
                };
                await EmailHelper.SendInvite(message);
                NotificationHelper.SendInviteEmail(invitation.Email, userId);

                return RedirectToAction("Details", "HouseHolds", new { id = currentUser.HouseholdId });
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitation.HouseholdId);
            return View(invitation);
        }

        // GET: Invitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitation.HouseholdId);
            return View(invitation);
        }

        //TODO - Test sending and receiving invitations
        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedDate,Email,Code,HouseholdId,Expired,Accepted")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitation.HouseholdId);
            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            db.Invitations.Remove(invitation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
