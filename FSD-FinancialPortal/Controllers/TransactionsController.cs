using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FSD_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using FSD_FinancialPortal.Helpers;

namespace FSD_FinancialPortal.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index(int accountId)
        {
            var householdId = db.BankAccounts.Find(accountId).HouseholdId;
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            if (user.HouseholdId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (user.HouseholdId != householdId)
            {
                return RedirectToAction("Details", "Home", new { id = user.HouseholdId });
            }

            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.BudgetItem).Include(t => t.EnteredBy).Where(t => t.BankAccountId == accountId);
            ViewBag.account = db.BankAccounts.Find(accountId);
            if (db.Transactions.Count() > 0)
            {
                ViewBag.maxAmount = db.Transactions.Where(t => t.BankAccountId == accountId).Select(t => t.Amount).Max();
                ViewBag.maxDate = db.Transactions.Where(t => t.BankAccountId == accountId).Select(t => t.Created).Max().Date;
            }
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems.Where(b => b.HouseholdId == householdId).ToList(), "Id", "Name", "Budget.Name", null, null);

            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name");
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name");
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Date,Amount,Reconciled,ReconciledAmount,BankAccountId,TransactionTypeId,BudgetItemId,EnteredById")] Transaction transaction, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                transaction.Created = Convert.ToDateTime(transaction.Created);
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var userId = User.Identity.GetUserId();
                var householdId = (int)db.Users.FirstOrDefault(u => u.Id == userId).HouseholdId;
                NotificationHelper.AddTransaction(userId, householdId, transaction.Id);

                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Bankaccounts", new { householdId = db.BankAccounts.Find(transaction.BankAccountId).HouseholdId });
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FirstName", transaction.EnteredById);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FirstName", transaction.EnteredById);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type", transaction.TransactionTypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Date,Amount,Reconciled,ReconciledAmount,BankAccountId,TransactionTypeId,BudgetItemId,EnteredById")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FirstName", transaction.EnteredById);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
