using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSD_FinancialPortal.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }


        //public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        //public TransactionType()
        //{
        //    this.Transactions = new HashSet<Transaction>();
        //    this.BudgetItems = new HashSet<BudgetItem>();
        //}
    }
}