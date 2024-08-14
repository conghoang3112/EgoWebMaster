using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums.EnumAttribute;

namespace TLS.Common.Enums
{
    public enum MediaChannel
    {
        [EnumDisplayName(DisplayName = "Facebook")]
        Facebook = 1,
        [EnumDisplayName(DisplayName = "Instagram")]
        Instagram = 2,
        [EnumDisplayName(DisplayName = "Tiktok")]
        Tiktok = 3,
        [EnumDisplayName(DisplayName = "PR/ Báo chí")]
        News = 4,
        [EnumDisplayName(DisplayName = "Website")]
        Website = 5
    }
}
