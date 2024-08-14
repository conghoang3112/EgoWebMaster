using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums.EnumAttribute;

namespace TLS.Common.Enums
{
    public enum BusinessField
    {
        [EnumDisplayName(DisplayName ="Giáo dục")]
        Education = 1,
        [EnumDisplayName(DisplayName ="Thẩm mỹ")]
        Cosmetics = 2,
        [EnumDisplayName(DisplayName ="Công nghệ")]
        Tech = 3,
        [EnumDisplayName(DisplayName ="Thời trang")]
        Fashion = 4,
        [EnumDisplayName(DisplayName = "Gia dụng")]
        Appliances = 5,
        [EnumDisplayName(DisplayName = "B2B")]
        B2B = 6
    }
}
