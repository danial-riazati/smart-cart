using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class Factor
    {
        public Factor()
        {
            CheckInOuts = new HashSet<CheckInOut>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public byte IsSucceed { get; set; }

        public virtual ICollection<CheckInOut> CheckInOuts { get; set; }
    }
}
