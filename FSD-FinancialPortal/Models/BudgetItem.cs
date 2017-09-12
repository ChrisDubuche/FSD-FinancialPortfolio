using System;
using System.Collections.Generic;

namespace FSD_FinancialPortal.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public int? HouseholdId { get; set; }
        public int? BudgetId { get; set; }
        public bool IsExpense { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        //Nav
        public virtual Budget Budget { get; set; }
        public virtual Household Household { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    }
}