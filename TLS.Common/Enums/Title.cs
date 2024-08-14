using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums.EnumAttribute;

namespace TLS.Common.Enums
{
    public enum Title
    {
        [EnumDisplayName(DisplayName = "Chủ doanh nghiệp")]
        BusinessOwner = 1,
        [EnumDisplayName(DisplayName = "CEO")]
        CEO = 2,
        [EnumDisplayName(DisplayName = "MarketingManager")]
        MarketingManager = 3
    }
}
