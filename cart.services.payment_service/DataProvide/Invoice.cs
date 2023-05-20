using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.payment_service.DataProvide
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime? Date { get; set; }
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public int? IsSucceed { get; set; }
        public int? TotalAmount { get; set; }
    }
}
