using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSD_FinancialPortal.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }

        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }


        public int HouseholdId { get; set; }
        public string OwnerId { get; set; }

        //Nav
        public virtual ApplicationUser Owner { get; set; }
        public virtual Household Household { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    }
}