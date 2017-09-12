using System;
using System.Collections.Generic;

namespace FSD_FinancialPortal.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public bool Active { get; set; }

        //Nav

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        public Household()
        {
            this.Budgets = new HashSet<Budget>();
            this.BankAccounts = new HashSet<BankAccount>();
            this.Members = new HashSet<ApplicationUser>();
            this.Invitations = new HashSet<Invitation>();
        }
    }
}