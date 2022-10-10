using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class FailureReport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CartId { get; set; }
        public byte IsFixed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? FixedDate { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
