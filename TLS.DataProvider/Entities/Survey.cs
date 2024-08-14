using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums;

namespace TLS.DataProvider.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; } = string.Empty;
        public string CompanyName { get; set; }
        public  BusinessField? BusinessField { get; set; }
        public Title? Title { get; set; }
        public MediaChannel? MediaChannel { get; set; }
        public string MainMediaChannelLink { get; set; }
        public ConversionMediaChannel? ConversionMediaChannel { get; set; }
        public string ConversionMediaChannelLink { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
