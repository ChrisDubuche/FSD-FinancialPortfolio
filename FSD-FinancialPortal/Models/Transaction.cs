using System;

namespace FSD_FinancialPortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Memo { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public decimal Amount { get; set; }
        public bool Voided { get; set; }

        public bool Deleted { get; set; }
        public bool Reconciled { get; set; }
        public decimal ReconciledAmount { get; set; }
        public string OwnerId { get; set; }
        public int BankAccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public int BudgetItemId { get; set; }
        public string EnteredById { get; set; }

        //Nav
        public virtual BankAccount BankAccount { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}