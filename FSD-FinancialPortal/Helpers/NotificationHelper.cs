using FSD_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSD_FinancialPortal.Helpers
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void JoinedHousehold(int? householdId, string userId)
        {
            var Household = db.Households.Find(householdId);
            var addedUser = db.Users.Find(userId);
            foreach (var user in Household.Members.SkipWhile(u => u == addedUser).ToList())
            {
                var notification = new Notification()
                {
                    Created = DateTime.Now,
                    Subject = "<strong>" + addedUser.FullName + "</strong>" + " has joined the household!",
                    SenderId = user.Id
                };
                db.Notifications.Add(notification);
            }

            db.SaveChanges();
        }

        public static void JoinedHouseholdNewUser(int? householdId, string userId)
        {
            var Household = db.Households.Find(householdId);
            var addedUser = db.Users.Find(userId);
            foreach (var user in Household.Members.SkipWhile(u => u == addedUser).ToList())
            {
                var notification = new Notification()
                {
                    Created = DateTime.Now,
                    Subject = "<strong>" + addedUser.FullName + "</strong>" + " has joined the household!",
                    SenderId = user.Id
                };
                db.Notifications.Add(notification);
            }

            db.SaveChanges();
        }

        public static void SendInviteEmail(string email, string userId)
        {
            var notification = new Notification()
            {
                Created = DateTime.Now,
                Subject = "An invite Email has been sent to " + "<strong>" + email + "</strong>",
                SenderId = userId
            };
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static void LeftHousehold(int HouseholdId, string userId)
        {
            var household = db.Households.Find(HouseholdId);

            foreach (var user in household.Members.ToList())
            {
                var notification = new Notification()
                {
                    Created = DateTime.Now,
                    Subject = "<strong>" + db.Users.Find(userId).FullName + "</strong>" + " has left the household",
                    SenderId = user.Id
                };
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }

        public static void NewBankAccount(int householdId, string userId, string bankName)
        {
            var household = db.Households.Find(householdId);

            foreach (var user in household.Members.ToList())
            {
                var notification = new Notification()
                {
                    Created = DateTime.Now,
                    Subject = "<strong>" + db.Users.Find(userId).FirstName + "</strong> has added a new account: " + "<strong>" + bankName + "</strong>",
                    SenderId = user.Id
                };
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }

        public static void Overdraft(int householdId, int accountId, decimal balance)
        {
            var household = db.Households.Find(householdId);
            var account = db.BankAccounts.Find(accountId);
            var oldNotification = db.Notifications.OrderByDescending(n => n.Created).FirstOrDefault(n => n.Account == accountId);
            decimal oldBalance;

            if (oldNotification == null)
            {
                oldBalance = 0;
            }
            else
            {
                oldBalance = (decimal)oldNotification.Balance;
            }          
        }

        public static void DeleteAccount(int householdId, string bankName)
        {
            var household = db.Households.Find(householdId);

            foreach (var user in household.Members.ToList())
            {
                var notification = new Notification()
                {
                    Created = DateTime.Now,
                    Subject = "<strong>" + bankName + "</strong>" + " has been deleted",
                    SenderId = user.Id
                };
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }

        public static void AddTransaction(string userId, int householdId, int transactionId)
        {
            var household = db.Households.Find(householdId);
            var transaction = db.Transactions.Find(transactionId);
            foreach (var user in household.Members.ToList())
            {
                var notification = new Notification()
                {
                    Created = DateTime.Now,
                    SenderId = user.Id
                };

                if (transaction.Voided)
                {
                    notification.Subject = "Transaction by<strong> " + db.Users.Find(userId).FirstName + "</strong> in<strong> " + db.Transactions.Find(transactionId).BudgetItem.Name + "</strong> for <strong> " + db.Transactions.Find(transactionId).Amount + "</strong> in<strong> " + transaction.BankAccount.Name + "</strong>";
                }
                else
                {
                    notification.Subject = "An income of<strong> " + transaction.Amount + "</strong> has been added to<strong> " + transaction.BankAccount.Name + "</strong>";
                }
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }
    }
}