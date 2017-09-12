using FSD_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FSD_FinancialPortal.Helpers
{
    public class Utilities
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetUserNameFromId(string id)
        {
            var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            return user.FirstName + " " + user.LastName;
        }

        public static string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public static int GetBudgetItemIdByName(string name)
        {
            return db.BudgetItems.FirstOrDefault(bi => bi.Name == name).Id;
        }

        public static int GetBankAccountIdByName(string name)
        {
            return db.BankAccounts.FirstOrDefault(ba => ba.Name == name).Id;
        }

        public static void ClearBudgetItems()
        {
            var allBudgetItems = db.BudgetItems.ToList();
            if (allBudgetItems.Count > 0)
            {
                foreach (var budgetItem in allBudgetItems)
                {
                    db.BudgetItems.Remove(budgetItem);
                }
                db.SaveChanges();
            }
        }

        public static void SeedBudgets(int hhId, string userId)
        {
            //Delete all BudgetItems
            var allBudgets = db.Budgets.ToList();
            try
            {

                if (allBudgets.Count > 0)
                {
                    foreach (var budget in allBudgets)
                    {
                        db.Budgets.Remove(budget);
                    }
                }

                var newBudget = new Budget();
                newBudget.Name = "Utilities";
                newBudget.Description = "This Budget tracks all household Utility costs";
                newBudget.Created = DateTime.Now;
                newBudget.HouseholdId = hhId;
                newBudget.TargetAmount = 500M;
                newBudget.CurrentAmount = 0M;
                newBudget.OwnerId = userId;
                db.Budgets.Add(newBudget);

                newBudget = new Budget();
                newBudget.Name = "Automotive";
                newBudget.Description = "This Budget tracks all Automotive costs";
                newBudget.Created = DateTime.Now;
                newBudget.HouseholdId = hhId;
                newBudget.TargetAmount = 1000M;
                newBudget.CurrentAmount = 0M;
                newBudget.OwnerId = userId;
                db.Budgets.Add(newBudget);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogHelper.AppendToLog(ex.InnerException.Message);
                LogHelper.AppendToLog(ex.StackTrace);
            }
        }

        public static void SeedBudgetItems(int hhId, string userId)
        {
            var utilityId = db.Budgets.AsNoTracking().FirstOrDefault(h => h.Name == "Utilities").Id;
            var autoId = db.Budgets.AsNoTracking().FirstOrDefault(h => h.Name == "Automotive").Id;


            var newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Gas Bill";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = utilityId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Water Bill";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = utilityId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Electric Bill";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = utilityId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Cable Bill";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = utilityId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Internet Bill";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = utilityId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Phone Bill";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = utilityId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Auto Fuel";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = autoId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Car Payment";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = autoId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            newBudgetItem = new BudgetItem();
            newBudgetItem.Name = "Car Insurance";
            newBudgetItem.HouseholdId = hhId;
            newBudgetItem.BudgetId = autoId;
            newBudgetItem.Created = DateTime.Now;
            newBudgetItem.TargetAmount = 150M;
            newBudgetItem.CurrentAmount = 0M;
            newBudgetItem.OwnerId = userId;
            db.BudgetItems.Add(newBudgetItem);

            db.SaveChanges();

        }

        public static void SeedAccounts(int hhId, string userId)
        {
            var allAccounts = db.BankAccounts.ToList();

            if (allAccounts.Count > 0)
            {
                foreach (var bankAccount in allAccounts)
                {
                    db.BankAccounts.Remove(bankAccount);
                }
                db.SaveChanges();
            }

            try
            {

                var newBankAccount = new BankAccount();
                newBankAccount.Name = "Wells Fargo Checking";
                newBankAccount.Created = DateTime.Now;
                newBankAccount.Updated = DateTime.Now;
                newBankAccount.StartingBalance = 5000M;
                newBankAccount.CurrentBalance = 5000M;
                newBankAccount.HouseholdId = hhId;
                newBankAccount.OwnerId = userId;
                db.BankAccounts.Add(newBankAccount);

                newBankAccount = new BankAccount();
                newBankAccount.Name = "FCU Savings";
                newBankAccount.Created = DateTime.Now;
                newBankAccount.Updated = DateTime.Now;
                newBankAccount.StartingBalance = 10000M;
                newBankAccount.CurrentBalance = 10000M;
                newBankAccount.HouseholdId = hhId;
                newBankAccount.OwnerId = userId;
                db.BankAccounts.Add(newBankAccount);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.AppendToLog(ex.InnerException.Message);
                LogHelper.AppendToLog(ex.StackTrace);
            }
        }

        public static void SeedTransactions(string userId)
        {
            //First Delete all Transactions
            var allTransactions = db.Transactions.ToList();


            if (allTransactions.Count > 0)
            {
                foreach (var transaction in allTransactions)
                {
                    db.Transactions.Remove(transaction);
                }
                db.SaveChanges();
            }

            var newTransaction = new Transaction
            {
                Memo = "Paid Gas Bill",
                OwnerId = userId,
                Amount = 100M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Gas Bill")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Pater Water Bill",
                OwnerId = userId,
                Amount = 75M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Water Bill")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Paid Electric Bill",
                OwnerId = userId,
                Amount = 80M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Electric Bill")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Paid Cable Bill",
                OwnerId = userId,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Cable Bill")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Paid Internet Bill",
                OwnerId = userId,
                Amount = 80M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Internet Bill")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Paid Phone Bill",
                OwnerId = userId,
                Amount = 40M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Phone Bill")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Gas (Buick)",
                OwnerId = userId,
                Amount = 40M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Auto Fuel")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Buick Car Payment",
                OwnerId = userId,
                Amount = 300M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Car Payment")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Buick Car Insurance",
                OwnerId = userId,
                Amount = 80M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Car Insurance")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Gas (Pontiac)",
                OwnerId = userId,
                Amount = 45M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Auto Fuel")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Pontiac Car Payment",
                OwnerId = userId,
                Amount = 275M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Car Payment")
            };
            db.Transactions.Add(newTransaction);

            newTransaction = new Transaction
            {
                Memo = "Pontiac Car Insurance",
                OwnerId = userId,
                Amount = 80M,
                Voided = false,
                Deleted = false,
                Reconciled = false,
                Created = DateTime.Now,
                BankAccountId = Utilities.GetBankAccountIdByName("Wells Fargo Checking"),
                BudgetItemId = Utilities.GetBudgetItemIdByName("Car Insurance")
            };
            db.Transactions.Add(newTransaction);
            db.SaveChanges();
        }

        public static decimal GetCurrentBudgetTotal(string budgetName)
        {
            var ttlAmt = 0M;
            var budgetId = db.Budgets.AsNoTracking().FirstOrDefault(b => b.Name == budgetName).Id;
            var BudgetItemIds = db.BudgetItems.AsNoTracking().Where(b => b.BudgetId == budgetId).Select(b => b.Id);
            foreach (var budgetItemId in BudgetItemIds.ToList())
            {
                ttlAmt += db.Transactions.AsNoTracking().Where(b => b.BudgetItemId == budgetItemId).Sum(b => b.Amount);
            }
            return ttlAmt;
        }

        public static decimal GetCurrentBudgetItemTotal(string budgetItemName)
        {
            var ttlAmt = 0M;
            var budgetItemId = db.BudgetItems.AsNoTracking().FirstOrDefault(b => b.Name == budgetItemName).Id;
            var transactions = db.Transactions.AsNoTracking().Where(t => t.BudgetItemId == budgetItemId);
            foreach (var transaction in transactions)
            {
                ttlAmt += transaction.Amount;
            }
            return ttlAmt;
        }

        public static decimal GetBudgetItemTargetAmt(string budgetItemName)
        {
            return db.BudgetItems.AsNoTracking().FirstOrDefault(b => b.Name == budgetItemName).TargetAmount;
        }

    }
}