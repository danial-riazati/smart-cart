using System;
using System.Collections.Generic;

#nullable disable

namespace cart.services.product_service.DataProvide
{
    public partial class Version
    {
        public Version(string versionName, string versionNumber, DateTime? date, string url)
        {
            VersionName = versionName;
            VersionNumber = versionNumber;
            Date = date;
            Url = url;
        }

        public string VersionName { get; set; }
        public string VersionNumber { get; set; }
        public DateTime? Date { get; set; }
        public string Url { get; set; }
    }
}
