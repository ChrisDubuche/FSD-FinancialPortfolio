using System;

namespace FSD_FinancialPortal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public string SenderId { get; set; }
        public string RecipientId { get; set; }

        public bool Acknowledged { get; set; }
        public int? Account { get; set; }
        public decimal? Balance { get; set; }

        //Nav
        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
    }
}