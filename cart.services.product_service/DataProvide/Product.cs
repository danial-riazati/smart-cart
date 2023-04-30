using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.product_service.DataProvide
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public string Barcode { get; set; }
    }
}
