using FSD_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSD_FinancialPortal.Helpers
{
    public class BudgetHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static decimal BudgetItemExpense(int budgetItemId)
        {
            var budgetItem = db.BudgetItems.FirstOrDefault(b => b.Id == budgetItemId);
            decimal amount = 0;
            foreach (var transaction in budgetItem.Transactions.ToList())
            {
                if (transaction.ReconciledAmount == 0)
                {
                    amount += transaction.Amount;
                }
                else
                {
                    amount += transaction.ReconciledAmount;
                }
            }

            return amount;
        }
    }
}