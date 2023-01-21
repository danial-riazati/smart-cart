using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class Checkin
    {
        public Checkin()
        {
            Checkouts = new HashSet<Checkout>();
        }

        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public byte IsCheckedOut { get; set; }
        public int CartId { get; set; }

        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}
