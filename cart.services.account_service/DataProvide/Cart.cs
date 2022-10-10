using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class Cart
    {
        public Cart()
        {
            CheckInOuts = new HashSet<CheckInOut>();
            FailureReports = new HashSet<FailureReport>();
        }

        public int Id { get; set; }
        public string AppVersion { get; set; }
        public byte InUse { get; set; }
        public int StoreType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<CheckInOut> CheckInOuts { get; set; }
        public virtual ICollection<FailureReport> FailureReports { get; set; }
    }
}
