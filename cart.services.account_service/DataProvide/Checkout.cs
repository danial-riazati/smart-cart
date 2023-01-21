using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class Checkout
    {
        public int Id { get; set; }
        public int CheckinId { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceId { get; set; }
        public int PayementId { get; set; }
        public int? TotalShopValue { get; set; }

        public virtual Checkin Checkin { get; set; }
    }
}
