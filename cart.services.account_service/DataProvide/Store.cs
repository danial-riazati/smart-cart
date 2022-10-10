using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class Store
    {
        public Store()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
