using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte IsPublished { get; set; }
    }
}
