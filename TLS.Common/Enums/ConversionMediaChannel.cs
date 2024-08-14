using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums.EnumAttribute;

namespace TLS.Common.Enums
{
    public enum ConversionMediaChannel
    {
        [EnumDisplayName(DisplayName = "Facebook")]
        Facebook = 1,
        [EnumDisplayName(DisplayName = "Instagram")]
        Instagram = 2,
        [EnumDisplayName(DisplayName = "Tiktok")]
        Tiktok = 3,
        [EnumDisplayName(DisplayName = "Website")]
        Website = 4,
        [EnumDisplayName(DisplayName = "Điểm bán")]
        Shop = 5,
        [EnumDisplayName(DisplayName = "Sàn TMDT")]
        EcommerceLevel = 6,
    }
}
