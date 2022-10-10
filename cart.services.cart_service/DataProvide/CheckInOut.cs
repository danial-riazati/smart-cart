using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class CheckInOut
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Browser { get; set; }
        public int CartId { get; set; }
        public DateTime CheckinDate { get; set; }
        public byte IsCheckedOut { get; set; }
        public byte IsFailed { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public int FactorId { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Factor Factor { get; set; }
    }
}
