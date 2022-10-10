using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.invoice_service.DataProvide
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public byte IsSucceed { get; set; }
        public int TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
