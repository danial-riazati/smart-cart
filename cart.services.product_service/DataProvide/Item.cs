using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.product_service.DataProvide
{
    public partial class Item
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Bytes { get; set; }
    }
}
