using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSD_FinancialPortal.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Code { get; set; }
        public int HouseholdId { get; set; }

        public bool Expired { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool Accepted { get; set; }
        public DateTime? AcceptedDate { get; set; }

        //Nav
        public virtual Household Household { get; set; }

        public Invitation()
        {
            Code = Guid.NewGuid().ToString();
        }

    }
}