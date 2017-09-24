using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FSD_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using FSD_FinancialPortal.Helpers;

namespace FSD_FinancialPortal.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public ActionResult Index(int householdId)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            if (user.HouseholdId == null)
            {
                return RedirectToAction("index", "home");
            }
            if (user.HouseholdId != householdId)
            {
                return RedirectToAction("details", "home", new { id = user.HouseholdId });
            }
            var bankAccounts = db.BankAccounts.Where(b => b.HouseholdId == householdId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems.Where(b => b.HouseholdId == householdId).ToList(), "Id", "Name", "Budget.Name", null, null);

            return View(bankAccounts.OrderBy(b => b.Id).ToList());
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AccountNumber,StartingBalance,CurrentBalance,HouseholdId")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();
                NotificationHelper.NewBankAccount(bankAccount.HouseholdId, User.Identity.GetUserId(), bankAccount.Name);
                return RedirectToAction("index", "BankAccounts", new { id = bankAccount.HouseholdId });
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccount.HouseholdId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccount.HouseholdId);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,City,State,Zip,Phone,RoutingNumber,AccountNumber,StartingBalance,CurrentBalance,Created,Updated,HouseholdId")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccount.HouseholdId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            NotificationHelper.DeleteAccount(bankAccount.HouseholdId, bankAccount.Name);
            db.BankAccounts.Remove(bankAccount);
            db.SaveChanges();
            return Redirect(returnUrl);
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
